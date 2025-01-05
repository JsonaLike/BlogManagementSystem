using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Core.Models
{
    public class BaseAuditableEntity:EntityBase   
    {
        [Column(Order = 201)]
        public DateTime? UpdatedAt { get; set; }

        [Column(Order = 202)]
        public string? UpdatedBy { get; set; }

        [Column(Order = 203)]
        public bool IsDeleted { get; set; }
    }
}