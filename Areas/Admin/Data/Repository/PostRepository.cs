using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public class PostRepository : IPostRespository
    {
        private readonly MyBlogContext _context;

        public PostRepository(MyBlogContext context)
        {
            _context = context;
        }

 

        public Post GetPostById(int id)
        {
            using var context = _context;
            return context.Posts
                .FirstOrDefault(o => o.PostId == id);
        }

        public IEnumerable<Post> GetPostsList()
        {
            var posts = _context.Posts
                .Include(p => p.Cat) // Include the Cat navigation property
                .OrderBy(x => x.PostId)
                .ToList();

            return posts;
        }

        public void InsertPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void DeletePost(int id)
        {
            var existingPost = _context.Posts.Find(id);
            if (existingPost != null)
            {
                _context.Posts.Remove(existingPost);
                _context.SaveChanges();
            }
        }

        public void UpdatePost(int id, Post post)
        {
            var existingPost = _context.Posts.Find(id);

            if (existingPost != null)
            {
                // Update the properties of the existing post
                existingPost.Title = post.Title;
                existingPost.Contents = post.Contents;
                existingPost.Thumb = post.Thumb;
                existingPost.Author = post.Author;
                existingPost.Published = post.Published;
                existingPost.CatId = post.CatId;
                existingPost.IsHot = post.IsHot;
                existingPost.IsNewFeed = post.IsNewFeed;

                // No need to create a new context or call SaveChanges separately
                _context.SaveChanges();
            }
        }

    }
}
