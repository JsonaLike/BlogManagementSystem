using Domain.Enums;
using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public PostStatus Status { get; set; }
        public byte[]? Image { get; set; }
        public Guid? AuthorId { get; set; }
        public Author? Author { get; set; }
        public List<Category> Categories { get; set; }
    }
}
