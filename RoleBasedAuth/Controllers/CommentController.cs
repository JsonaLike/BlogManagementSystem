using Application.Core.Services;
using Microsoft.AspNetCore.Mvc;
using MT.Innovation.WebApiAdmin.Framework;

namespace BlogManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseApiController
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("post/{postId:guid}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            var comments = await _commentService.GetCommentsByPostId(postId);
            return Ok(comments);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentId = await _commentService.AddComment(commentDto.PostId,commentDto.Name ,commentDto.Content);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentId }, new { Id = commentId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _commentService.UpdateComment(id, updateDto.Content);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            try
            {
                await _commentService.DeleteComment(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }

    public class CommentDto
    {
        public Guid PostId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class UpdateCommentDto
    {
        public string Content { get; set; }
    }
}
