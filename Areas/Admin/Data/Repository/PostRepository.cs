using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using System;
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

        public IQueryable<Post> GetPostsList()
        {
            var posts = _context.Posts
                .Include(p => p.Cat) // Include the Cat navigation property
                .OrderBy(x => x.PostId)
                .AsQueryable();

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
                existingPost.Title = post.Title;
                existingPost.Contents = post.Contents;
                existingPost.Thumb = post.Thumb;
                existingPost.Author = post.Author;
                existingPost.Published = post.Published;
                existingPost.CatId = post.CatId;
                existingPost.IsHot = post.IsHot;
                existingPost.IsNewFeed = post.IsNewFeed;
                _context.SaveChanges();
            }
        }

        public Post GetLastPost()
        {
            var post = _context.Posts.OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return post;
        }
        public IEnumerable<Post> GetRecentPosts(int count)
        {
            var posts = _context.Posts.OrderByDescending(p => p.CreateTime).Take(count).ToList();
            return posts;
        }

        public IEnumerable<Post> GetHotPosts(int count)
        {
            var hotPosts = _context.Posts
                .Where(p => p.IsHot == true)
                .Take(count)
                .ToList();

            return hotPosts;
        }

        public IEnumerable<Post> GetNewFeedPosts(int count)
        {
            var hotPosts = _context.Posts
      .Where(p => p.IsNewFeed == true)
      .Take(count)
      .ToList();

            return hotPosts;
        }
    }
}
