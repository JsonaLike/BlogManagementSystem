using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MT.Innovation.Shared.Infrastructure;

namespace ERP.Infrastructure.Data
{
    public class DataInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly IEncryptionService _encryptionService;
        public ApplicationInfo _applicationInfo { get; private set; }
        public DataInitializer(IServiceProvider serviceProvider, ApplicationInfo applicationInfo)
        {
            _serviceProvider = serviceProvider;
            _dbContextOptions = _serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            _encryptionService = _serviceProvider.GetRequiredService<IEncryptionService>();
            _applicationInfo = applicationInfo;
        }

        public void Initialize()
        {
            using (var dbContext = new ApplicationDbContext(_dbContextOptions, _applicationInfo))
            {
               if (dbContext.Database.CanConnect())
                {
                    dbContext.Database.Migrate();
                    return;
                }

                dbContext.Database.Migrate();
                var roleId = Guid.NewGuid();
                dbContext.Roles.Add(new Role
                {
                    Id = roleId,
                    Name = "Super Admin",
                    Description = "This Role has all rights"
                });
                dbContext.SaveChanges();

                #region Add Super Admin
                var saltKey = _encryptionService.CreateSaltKey(5);
                var admin = new User()
                {
                    UserName = "Admin",
                    SaltKey = saltKey,
                    PasswordHash = _encryptionService.CreatePasswordHash("Password", saltKey),
                    IsSuperUser = true,
                };
                dbContext.Users.Add(admin);
                dbContext.SaveChanges();
                dbContext.UserRoles.Add(new UserRole()
                {
                    Id = Guid.NewGuid(),
                    UserId = admin.Id,
                    RoleId = roleId
                });
                dbContext.SaveChanges();
                #endregion
                roleId = Guid.NewGuid();
                dbContext.Roles.Add(new Role
                {
                    Id = roleId,
                    Name = "Author",
                    Description = "This Role has the right to post"
                });
                dbContext.SaveChanges();
            }
        }
    }
}
