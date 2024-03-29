﻿using MyBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Areas.Admin.Data.Repository
{
    public interface ICategoriesRespository
    {
        int GetCountCategory();
        IQueryable<Category> GetCatList();
        void InsertCat(Category cat);
        void UpdateCat(int id, Category cat);
        void DeleteCat(int id);
        Category GetCateById(int? catId);
        List<Category> LoadCatgories();

    }
}
