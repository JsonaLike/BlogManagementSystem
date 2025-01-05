using Domain.Dtos;
using Domain.Entities;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Users;
using ERP.Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using MT.Innovation.Shared.Infrastructure;
using MT.Innovation.Shared.Utils;

namespace Application.Core.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _dbContext;
        public IEncryptionService _encryptionService { get; set; }

        public AuthorService(ApplicationDbContext dbContext, IEncryptionService encryptionService)
        {
            _dbContext = dbContext;
            _encryptionService = encryptionService;
        }

        public bool CreateAuthor(CreateUpdateAuthorDto author)
        {
            if (_dbContext.Users.Any(x => x.UserName == author.UserName))
            {
                throw new Exception("Author with the same username already exists.");
            }
            var saltkey = Guid.NewGuid().ToString();
            var passwordHash = _encryptionService.CreatePasswordHash(author.Password, saltkey);
            var user = new User
            {
                UserName = author.UserName,
                PasswordHash = passwordHash,
                SaltKey = saltkey
            };
            _dbContext.Users.Add(user);
            byte[] Image;
            _dbContext.SaveChanges();

            using (var memoryStream = new MemoryStream())
            {
                author.image.CopyTo(memoryStream);
                Image = memoryStream.ToArray();
            }
            _dbContext.Authors.Add(new Author
            {
                UserId = user.Id,
                Image = Image,
            });
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<PagedList<Author>> GetAuthors(SearchCriteriaBase criteria)
        {
            return await _dbContext.Authors.ToPagedListAsync(criteria.PageNumber, criteria.PageSize);
        }

        public User GetAuthorById(Guid authorId)
        {
            var author = _dbContext.Users.FirstOrDefault(x => x.Id == authorId);
            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            return author;
        }

        public bool UpdateAuthor(Guid authorId, CreateUpdateAuthorDto updatedAuthor)
        {
            var user = _dbContext.Authors.FirstOrDefault(x => x.Id == authorId);
            if (updatedAuthor.image != null)
            {
                byte[] image;
                using (var memoryStream = new MemoryStream())
                {
                    updatedAuthor.image.CopyTo(memoryStream);
                    image = memoryStream.ToArray();
                }

                user.Image = image;
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteAuthor(Guid authorId)
        {
            var author = _dbContext.Users.FirstOrDefault(x => x.Id == authorId);
            if (author == null)
            {
                throw new Exception("Author not found.");
            }

            _dbContext.Users.Remove(author);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
