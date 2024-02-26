using System.Collections.Generic;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public interface IPostRespository
    {
        IEnumerable<Post> GetPostsList();
        void InsertPost(Post post);
        //void UpdatePost(Post post);
        void DeletePost(int id);
        Post GetPostById(int id);
       void UpdatePost(int id, Post post);
    }
}
