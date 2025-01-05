using System.Linq.Expressions;
using Domain.Entities;
using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MT.Innovation.Shared.Infrastructure;

namespace ERP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationInfo _applicationInfo { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ApplicationInfo applicationInfo) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            _applicationInfo = applicationInfo;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable sensitive data logging
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Expression<Func<BaseAuditableEntity, bool>> filterExpr = x => !x.IsDeleted;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseEntity
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseAuditableEntity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
            modelBuilder.Entity<Post>()
                 .HasMany(e => e.Categories)
                 .WithMany(e => e.posts)
                 .UsingEntity<PostCategory>();
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.PreserveAuditFields(_applicationInfo);
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}