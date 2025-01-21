
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
        public string[]? Categories { get; set; }
    }
}
