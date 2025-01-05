using ERP.Domain.Core.Models;

namespace Domain.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

    }
}