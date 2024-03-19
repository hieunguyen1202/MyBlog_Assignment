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
            var post = _context.Posts.OrderByDescending(p => p.CreateTime).Where(p => p.Published == true).FirstOrDefault();
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
                  .OrderBy(p => Guid.NewGuid())
                .Take(count)
                .ToList();

            return hotPosts;
        }

        public IEnumerable<Post> GetNewFeedPosts(int count)
        {
            var hotPosts = _context.Posts
      .Where(p => p.IsNewFeed == true)
        .OrderBy(p => Guid.NewGuid())
      .Take(count)
      .ToList();

            return hotPosts;
        }

        public IEnumerable<Post> GetTags(int count)
        {
            var tags = _context.Posts
                               .Where(p => p.Tags != null)
                               .GroupBy(p => p.Tags)  // Group posts by tags
                               .Select(g => g.FirstOrDefault())  // Select the first post in each group
                               .Take(count)
                               .ToList();
            return tags;
        }

        public List<int?> GetCatIdList()
        {
            var catIdList = _context.Posts
                .Where(p => p.CatId != null)
                .Select(p => p.CatId).Distinct()
                .ToList();

            return catIdList;
        }

        public IEnumerable<Post> GetPostByCatId(int catId, int count)
        {
            var posts = _context.Posts
                .Where(p => p.CatId == catId && p.Published == true)
                  .OrderBy(p => Guid.NewGuid())
                .Take(count)
                .ToList();
            return posts;
        }


        public IEnumerable<Post> GetTop3Post(int count)
        {
            var randomPosts = _context.Posts
    .Where(p => p.Published != false)
    .OrderBy(p => Guid.NewGuid())
    .Take(count)
    .ToList();
            return randomPosts;
        }

        public int GetPostCount()
        {
            var count = _context.Posts.Count();
            return count;
        }

        public IQueryable<Post> SearchPost(string search)
        {
            var searchResult = _context.Posts
                                       .Where(p => (p.Title.Contains(search) || p.Contents.Contains(search)) && p.Published == true)
                                       .AsQueryable();
            return searchResult;
        }


    }
}
