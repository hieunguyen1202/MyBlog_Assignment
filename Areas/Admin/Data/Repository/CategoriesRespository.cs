using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public class CategoriesRespository : ICategoriesRespository
    {
        private readonly MyBlogContext _context;

        public CategoriesRespository(MyBlogContext context)
        {
            _context = context;
        }
        public Category GetCateById(int? id)
        {
            return _context.Categories.FirstOrDefault(o => o.CatId == id);
        }
        public void DeleteCat(int id)
        {
            using (var context = _context)
            {
                var existingCat = context.Categories.Find(id);
                if (existingCat != null)
                {
                    context.Categories.Remove(existingCat);
                    context.SaveChanges();
                }
            }
        }


    

    public IEnumerable<Category> GetCatList()
        {
            return _context.Categories.ToList();
        }
   
        public void InsertCat(Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
        }

        public void UpdateCat(int id, Category cat)
        {
            var existingCat = _context.Categories.Find(id);
            if (existingCat != null)
            {
                existingCat.Title = cat.Title;
                existingCat.CatName = cat.CatName;
                existingCat.Description = cat.Description;
                existingCat.Icon = cat.Icon;
                existingCat.Cover = cat.Cover;
                existingCat.Levels = cat.Levels;
                existingCat.Cover = cat.Cover;
                existingCat.Ordering = cat.Ordering;
                existingCat.Parents = cat.Parents;
                existingCat.Published = cat.Published;
                _context.SaveChanges();
            }
        }
    }
}
