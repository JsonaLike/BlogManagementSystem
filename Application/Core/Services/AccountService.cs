
using Domain.Dtos;
using ERP.Application.Core.Helpers;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Users;
using ERP.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Application.Core.Services
{
    public class AccountService
    {
        public ApplicationDbContext _dbContext { get; set; }
        public IEncryptionService _encryptionService { get; set; }
        private IConfiguration _config { get; set; }
        public AccountService(ApplicationDbContext DbContext, IEncryptionService encryptionService, IConfiguration configuration)
        {
            _dbContext = DbContext;
            _encryptionService = encryptionService;
            _config = configuration;
        }
        public string Login(UserLogin user)
        {
            var DbUser = _dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (DbUser == null)
            {
                throw new Exception("User Doesn't Exist");
            }
            var passwordHash = _encryptionService.CreatePasswordHash(user.Password, DbUser.SaltKey);
            if (passwordHash != DbUser.PasswordHash)
            {
                throw new Exception("Password Doesn't Match");
            }
            var secretKey = _config.GetValue<string>("JWTSecretKey");
            return JWTHelper.GenerateJwtToken(DbUser.Id.ToString(), secretKey);
        }
        public bool Register(UserRegister user)
        {
            var DbUser = _dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (DbUser != null)
            {
                throw new Exception("User Already Exists");
            }
            var saltkey = new Guid().ToString();
            var passwordHash = _encryptionService.CreatePasswordHash(user.Password, saltkey);
            var User = _dbContext.Users.Add(new User { UserName = user.UserName, PasswordHash = passwordHash , SaltKey = saltkey});
            _dbContext.SaveChanges();
            return true;

        }
        public async Task<bool> CreateAuthorUser(AuthorRegister user)
        {
            var DbUser = _dbContext.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (DbUser != null)
            {
                throw new Exception("User Already Exists");
            }

            var saltkey = Guid.NewGuid().ToString();
            var passwordHash = _encryptionService.CreatePasswordHash(user.Password, saltkey);

            var newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = passwordHash,
                SaltKey = saltkey
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            var authorRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "Author");
            if (authorRole == null)
            {
                throw new Exception("Role 'Author' does not exist. Please create the role first.");
            }

            _dbContext.UserRoles.Add(new UserRole
            {
                UserId = newUser.Id,
                RoleId = authorRole.Id
            });
            string fileContent;

            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await user.image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }
            var author = new Domain.Entities.Author
            {
                UserId = newUser.Id,
            };
            if (imageBytes != null)
                author.Image = imageBytes;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
