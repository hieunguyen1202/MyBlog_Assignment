using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MyBlogContext _context;

        public CommentRepository(MyBlogContext context)
        {
            _context = context;
        }
        public void createComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public IQueryable<Comment> GetCommentsList()
        {
            var posts = _context.Comments
                .Include(p => p.Post) 
                .OrderBy(x => x.CommentId)
                .AsQueryable();

            return posts;
        }
        public int GetCommentByEmailAndPost(string email, int postId)
        {
            // Assuming you have a list of comments stored somewhere
            List<Comment> comments = _context.Comments.ToList();

            int commentCount = comments.Count(comment => comment.Email == email && comment.PostId == postId);
            int count = commentCount;
            return commentCount;
        }

        public IEnumerable<Comment> GetCommentListByPostId(int postId)
        {
            return _context.Comments
             .Where(comment => comment.PostId == postId)
             .ToList();
        }
        public void DeleteComment(int id)
        {
            var existingComment = _context.Comments.Find(id);
            if (existingComment != null)
            {
                _context.Comments.Remove(existingComment);
                _context.SaveChanges();
            }
        }

        public int GetCountComment()
        {
           var count = _context.Comments.Count();
            return count;
        }
    }
}
