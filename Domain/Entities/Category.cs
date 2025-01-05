
using ERP.Domain.Core.Models;

namespace Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post> posts { get; set; }
    }
}
