using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Core.Services;
using ERP.Domain.Core.Services;
using Microsoft.AspNetCore.Http;
using MT.Innovation.Shared.Infrastructure;
using MT.Innovation.WebApiAdmin.Framework;
using MT.Innovation.Shared.Utils;
using Domain.Entities;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : BaseApiController
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        [RoleAuthorize("Super Admin")]
        public IActionResult CreateAuthor([FromForm] CreateUpdateAuthorDto authorDto)
        {
            try
            {
                var result = _authorService.CreateAuthor(authorDto);
                if (result)
                    return Ok(new { Message = "Author created successfully." });
                return BadRequest(new { Message = "Failed to create author." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [RoleAuthorize("Super Admin")]
        public async Task<IActionResult> GetAuthors([FromQuery] SearchCriteriaBase criteria)
        {
            try
            {
                PagedList<Author> authors = await _authorService.GetAuthors(criteria);
                return Ok(PagedResult(authors));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [RoleAuthorize("Super Admin")]
        public IActionResult GetAuthorById(Guid id)
        {
            try
            {
                var author = _authorService.GetAuthorByUserId(id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [RoleAuthorize("Super Admin")]
        public IActionResult UpdateAuthor(Guid id, [FromForm] CreateUpdateAuthorDto authorDto)
        {
            try
            {
                var result = _authorService.UpdateAuthor(id, authorDto);
                if (result)
                    return Ok(new { Message = "Author updated successfully." });
                return BadRequest(new { Message = "Failed to update author." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [RoleAuthorize("Super Admin")]
        public IActionResult DeleteAuthor(Guid id)
        {
            try
            {
                var result = _authorService.DeleteAuthor(id);
                if (result)
                    return Ok(new { Message = "Author deleted successfully." });
                return BadRequest(new { Message = "Failed to delete author." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = ex.Message });
            }
        }
    }
}
