using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Users;

namespace ERP.Domain.Modules.Roles
{
    public class Role : AggregateRoot
    {
        public Role()
        { }

        private Role(Guid id, string name, string? description, Guid createdBy)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedBy = createdBy.ToString();
        }

        

        #region States
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<UserRole> UserRoles { get; protected set; }
        #endregion
    }
}