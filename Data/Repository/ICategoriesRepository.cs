using System.Collections.Generic;

namespace MyBlog.Data.Repository
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetCatList();
    }
}
