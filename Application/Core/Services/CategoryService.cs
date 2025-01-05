using Domain.Entities;
using ERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services
{
    public class CategoryService
    {
        public ApplicationDbContext _applicationDbContext { get; set; }

        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // Create a new category
        public async Task<Category> CreateCategoryAsync(string name, string description)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
            _applicationDbContext.Categories.Add(category);
            await _applicationDbContext.SaveChangesAsync();
            return category;
        }

        // Get a category by ID
        public Category GetCategoryById(Guid categoryId)
        {
            return _applicationDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        // Get all categories
        public List<Category> GetAllCategories()
        {
            return _applicationDbContext.Categories.ToList();
        }

        // Update a category
        public async Task<bool> UpdateCategoryAsync(Guid categoryId, string newName, string description)
        {
            var category = GetCategoryById(categoryId);
            if (category == null) return false;

            category.Name = newName;
            category.Description = description;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        // Delete a category
        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = GetCategoryById(categoryId);
            if (category == null) return false;

            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        // Assign a post to a category
        public async Task<bool> AssignPostToCategoryAsync(Guid postId, Guid categoryId)
        {
            var post = _applicationDbContext.Posts.FirstOrDefault(p => p.Id == postId);
            var category = _applicationDbContext.Categories.Include(x => x.posts).FirstOrDefault(c => c.Id == categoryId);

            if (post == null || category == null) return false;
            category.posts.Add(post);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemovePostFromCategoryAsync(Guid postId, Guid categoryId)
        {
            var post = _applicationDbContext.Posts.FirstOrDefault(p => p.Id == postId);
            var category = _applicationDbContext.Categories.Include(x => x.posts).FirstOrDefault(c => c.Id == categoryId);

            if (post == null || category == null) return false;
            category.posts.Remove(post);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
