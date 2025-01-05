using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Modules.Users
{
    public class UserRole : BaseAuditableEntity
    {
        public UserRole()
        { }

        private UserRole(Guid id, Guid userId, Guid roleId, Guid createdBy)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
        }

        public static UserRole Create(Guid userId, Guid roleId, Guid createdBy)
        {

            return new UserRole(Guid.NewGuid(), userId, roleId, createdBy);
        }


        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; protected set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; protected set; }
    }
}