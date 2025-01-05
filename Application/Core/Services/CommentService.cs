using Domain.Entities;
using ERP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core.Services
{
    public class CommentService
    {
        public ApplicationDbContext _applicationDbContext { get; set; }

        public CommentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Adds a new comment to a post.
        /// </summary>
        public async Task<Guid> AddComment(Guid postId, string name, string content)
        {
            var comment = new Comment
            {
                Content = content,
                PostId = postId,
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            _applicationDbContext.Comments.Add(comment);
            await _applicationDbContext.SaveChangesAsync();

            return comment.Id;
        }

        /// <summary>
        /// Retrieves all comments for a specific post.
        /// </summary>
        public async Task<List<Comment>> GetCommentsByPostId(Guid postId)
        {
            return await _applicationDbContext.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single comment by its ID.
        /// </summary>
        public async Task<Comment> GetCommentById(Guid commentId)
        {
            var comment = await _applicationDbContext.Comments.FindAsync(commentId);

            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }

            return comment;
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        public async Task UpdateComment(Guid commentId, string updatedContent)
        {
            var comment = await _applicationDbContext.Comments.FindAsync(commentId);

            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }

            comment.Content = updatedContent;
            
            _applicationDbContext.Comments.Update(comment);
            await _applicationDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        public async Task DeleteComment(Guid commentId)
        {
            var comment = await _applicationDbContext.Comments.FindAsync(commentId);

            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }

            _applicationDbContext.Comments.Remove(comment);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
