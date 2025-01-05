using Application.Core.Services;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MT.Innovation.Shared.Infrastructure;
using MT.Innovation.Shared.Utils;
using MT.Innovation.WebApiAdmin.Framework;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseApiController
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Retrieves a paginated list of posts.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] SearchCriteriaBase searchCriteria)
        {
            PagedList<Post> posts = await _postService.GetList(searchCriteria);
            return Ok(PagedResult(posts));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var post = await _postService.GetById(id);
                return Ok(post);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postId = await _postService.CreatePost(createDto);
            return CreatedAtAction(nameof(GetById), new { id = postId }, new { Id = postId });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _postService.UpdatePost(id, updateDto);
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _postService.RemovePost(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
