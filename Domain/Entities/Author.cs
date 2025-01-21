using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author : EntityBase
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public byte[]? Image { get; set; }
    }
}
