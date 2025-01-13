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
        public PostService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                    Author = post.Author.UserName
                }).ToPagedListAsync(search.PageNumber, search.PageSize);
            return posts;
        }
        public async Task<Guid> CreatePost(CreatePostDto ideaDto)
        {
            var post = new Post { Title = ideaDto.Title, Content = ideaDto.Content, Status = PostStatus.Posted };
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
            var post = await _applicationDbContext.Posts.FindAsync(postId);
            return post;
        }
        public async Task UpdatePost(Guid postId, UpdatePostDto updateDto)
        {
            var post = await _applicationDbContext.Posts.FindAsync(postId);
            post.Title = updateDto.Title;
            post.Content = updateDto.Content;
            _applicationDbContext.Posts.Update(post);
            await _applicationDbContext.SaveChangesAsync();
        }

    }
}
