using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Application.Core.Services;
using Domain.Entities;
using Domain.Dtos;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Create a new category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateUpdateCategoryDto createCategoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(createCategoryDto.Name, createCategoryDto.Description);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // Get a category by ID
        [HttpGet("{id:guid}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // Get all categories
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        // Update a category
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateUpdateCategoryDto createCategoryDto)
        {
            var success = await _categoryService.UpdateCategoryAsync(id, createCategoryDto.Name, createCategoryDto.Description);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // Delete a category
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // Assign a post to a category
        [HttpPost("{categoryId:guid}/posts/{postId:guid}")]
        public async Task<IActionResult> AssignPostToCategory(Guid categoryId, Guid postId)
        {
            var success = await _categoryService.AssignPostToCategoryAsync(postId, categoryId);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // Remove a post from a category
        [HttpDelete("{categoryId:guid}/posts/{postId:guid}")]
        public async Task<IActionResult> RemovePostFromCategory(Guid categoryId, Guid postId)
        {
            var success = await _categoryService.RemovePostFromCategoryAsync(postId, categoryId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
