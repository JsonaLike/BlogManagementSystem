using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.Core.Models
{
    public class EntityBase
    {
        [Column(Order = 0)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 101)]
        public DateTime CreatedAt { get; set; }

        [Column(Order = 102)]
        public string? CreatedBy { get; set; }
    }
}