using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public interface IPostRespository
    {
        IQueryable<Post> GetPostsList();
        void InsertPost(Post post);
        //void UpdatePost(Post post);
        void DeletePost(int id);
        Post GetPostById(int id);
       void UpdatePost(int id, Post post);
        Post GetLastPost();
        IEnumerable<Post> GetRecentPosts(int count);
        IEnumerable<Post> GetHotPosts(int count);
        IEnumerable<Post> GetNewFeedPosts(int count);
    }
}
