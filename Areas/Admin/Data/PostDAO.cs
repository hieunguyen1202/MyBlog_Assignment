using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Areas.Admin.Data.Repository;

namespace MyBlog.Areas.Admin.Data
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static readonly object instanceLook = new object();

        private readonly MyBlogDbContext _myBlogDbContext;
        public PostDAO(MyBlogDbContext myBlogDbContext)
        {
            _myBlogDbContext = myBlogDbContext;
        }
        private PostDAO()
        {
        }

        public static PostDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new PostDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Post> GetPostsList()
        {
            IEnumerable<Post> postList;
            try
            {
                using var context = _myBlogDbContext;
                postList = context.PostObject.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return postList;
        }
        public void InsertPost(Post post)
        {
            using (var context = _myBlogDbContext)
            {

                context.PostObject.Add(post);
                context.SaveChanges();
            }
        }


        public void DeletePost(int id)
        {
            if (id != null)
            {
                using (var context = _myBlogDbContext)
                {
                    Post p = GetPostByID(id);
                    if (p != null)
                    {
                        context.PostObject.Remove(p);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Invalid post ID");
            }
        }
        public Post GetPostByID(int postId)
        {
            try
            {
                using var context = _myBlogDbContext;
                return context.PostObject.SingleOrDefault(p => p.PostId == postId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving post by ID: " + ex.Message);
            }
        }

        public Post UpdatePost(int id, Post updatedPost)
        {
            // Retrieve the existing post from the database
            var existingPost = GetPostByID(id);

            if (existingPost != null)
            {
                // Update the properties of the existing post
                existingPost.Title = updatedPost.Title;
                existingPost.Contents = updatedPost.Contents;
                existingPost.Thumb = updatedPost.Thumb;
                existingPost.Author = updatedPost.Author;
                existingPost.Published = updatedPost.Published;
                existingPost.CatId = updatedPost.CatId;
                existingPost.IsHot = updatedPost.IsHot;
                existingPost.IsNewFeed = updatedPost.IsNewFeed;

                using (var context = _myBlogDbContext)
                {
                    context.PostObject.Update(existingPost);
                    context.SaveChanges();
                }
            }

            return existingPost;
        }
    }
}
