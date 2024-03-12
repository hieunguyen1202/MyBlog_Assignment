using MyBlog.Models;
using System.Collections.Generic;

namespace MyBlog.Data.Repository
{
    public interface ICommentRepository
    {
        void createComment(Comment comment);
        IEnumerable<Comment> GetCommentListByPostId(int postId);
    }
}
