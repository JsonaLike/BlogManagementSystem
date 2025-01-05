using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Users;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public byte[] Image { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public List<Category> Categories { get; set; }
    }
}
