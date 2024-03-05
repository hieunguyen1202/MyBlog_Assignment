using MyBlog.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Data.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MyBlogContext _context;

        public CategoriesRepository(MyBlogContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCatList()
        {
            return _context.Categories.ToList();
        }
    }
}
