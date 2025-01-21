using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using ERP.Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using MT.Innovation.Shared.Infrastructure;
using MT.Innovation.Shared.Utils;

namespace Application.Core.Services
{
    public class PostService
    {
        public ApplicationDbContext _applicationDbContext { get; set; }
        public AuthorService _authorService { get; set; }
        public ApplicationInfo _applicationInfo { get; set; }
        public PostService(ApplicationDbContext applicationDbContext, AuthorService authorService,ApplicationInfo applicationInfo)
        {
            _applicationDbContext = applicationDbContext;
            _authorService = authorService;
            _applicationInfo = applicationInfo;
        }
        public async Task<PagedList<PostDto>> GetList(SearchCriteriaBase search)
        {
            PagedList<PostDto> posts = await _applicationDbContext.Posts
                .Include(x => x.Categories)
                .Select(post => new PostDto
                {
                    Id = post.Id.ToString(),
                    Title = post.Title,
                    Status = post.Status.ToString(),
                    Categories = post.Categories.Select(x => x.Name).ToArray(),
                    Author = post.Author.User.UserName
                }).ToPagedListAsync(search.PageNumber, search.PageSize);
            return posts;
        }
        public async Task<Guid> CreatePost(CreatePostDto ideaDto)
        {
            var author = _authorService.GetAuthorByUserId(Guid.Parse(_applicationInfo.CurrentUserId));
            var post = new Post { Title = ideaDto.Title, Content = ideaDto.Content, Status = PostStatus.Posted, Author = author };
            if(post.Categories != null)
            post.Categories = _applicationDbContext.Categories.Where(x => ideaDto.Categories.ToList().Contains(x.Id.ToString())).ToList();
            _applicationDbContext.Posts.Add(post);
            await _applicationDbContext.SaveChangesAsync();
            return post.Id;
        }
        public async Task RemovePost(Guid postId)
        {
            var post = await _applicationDbContext.Posts.FindAsync(postId);
            _applicationDbContext.Posts.Remove(post);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<Post> GetById(Guid postId)
        {
            var post = await _applicationDbContext.Posts.Include(x => x.Categories).FirstAsync(x => x.Id == postId);
            return post;
        }
        public async Task UpdatePost(Guid postId, UpdatePostDto dto)
        {
            var post = await _applicationDbContext.Posts.Include(x=>x.Categories).FirstAsync(x=>x.Id == postId);
            if (post == null)
            {
                throw new Exception("Post not found");
            }

            post.Title = dto.Title;
            post.Content = dto.Content;

            if (dto.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Image.CopyToAsync(memoryStream);
                    post.Image = memoryStream.ToArray();
                }
            }
            post.Categories = _applicationDbContext.Categories.Where(x => dto.Categories.ToList().Contains(x.Id.ToString())).ToList();
            _applicationDbContext.Posts.Update(post);
            await _applicationDbContext.SaveChangesAsync();
        }


    }
}
