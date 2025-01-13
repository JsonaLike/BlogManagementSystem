
namespace Domain.Dtos
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string[] Categories { get; set; }
        public string Author { get; set; }
    }
}
