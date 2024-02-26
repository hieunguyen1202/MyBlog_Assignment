using System.Collections.Generic;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public interface ICategoriesRespository
    {
        IEnumerable<Category> GetCatList();
        void InsertCat(Category cat);
        void UpdateCat(int id, Category cat);
        void DeleteCat(int id);
        Category GetCateById(int? catId);
    }
}
