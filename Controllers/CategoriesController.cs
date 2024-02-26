using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlog.Areas.Admin.Data.Repository;

namespace MyBlog.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRespository _repository;

        public CategoriesController(ICategoriesRespository repository)
        {
            _repository = repository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
