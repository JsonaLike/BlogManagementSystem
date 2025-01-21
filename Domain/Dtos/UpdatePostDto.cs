
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos
{
    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string[]? Categories { get; set; }
        public IFormFile? Image { get; set; }
    }
}
