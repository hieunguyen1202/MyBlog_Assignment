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

        public IEnumerable<Comment> GetCommentListByPostId(int postId)
        {
            return _context.Comments
             .Where(comment => comment.PostId == postId)
             .ToList();
        }
    }
}
