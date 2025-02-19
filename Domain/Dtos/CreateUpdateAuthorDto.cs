﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreateUpdateAuthorDto
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public IFormFile? image { get; set; }
    }
}
