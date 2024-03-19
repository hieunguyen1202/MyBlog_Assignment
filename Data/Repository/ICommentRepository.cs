using MyBlog.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Data.Repository
{
    public interface ICommentRepository
    {
        void createComment(Comment comment);
        IEnumerable<Comment> GetCommentListByPostId(int postId);
        int GetCommentByEmailAndPost(string email, int postId);
        IQueryable<Comment> GetCommentsList();
        void DeleteComment(int id);
        int GetCountComment();
    }
}
