using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Data
{
    public class MyBlogDbContext : DbContext
    {
        public MyBlogDbContext()
        {
        }

        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("Posts");
        }


        public DbSet<Account> AccountObject { get; set; }

        public DbSet<Category> CategoryObject { get; set; }

        public DbSet<Post> PostObject { get; set; }
    }


}
