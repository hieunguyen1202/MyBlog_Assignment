using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyBlog;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Models;
using NuGet.Protocol.Core.Types;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRespository _repository;

        public CategoriesController(ICategoriesRespository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("Manage")]
        [CustomeAuthen(View = "Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Category> cats =  _repository.GetCatList();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                cats = cats.Where(s => (s.Title != null && s.Title.Contains(searchString)) ||
                                     (s.CatName != null && s.CatName.Contains(searchString)));
            }
            if (!cats.Any())
            {
                ViewData["Result"] = "No records found.";
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cats = cats.OrderByDescending(s => s.CatName);
                    break;
                default:
                    cats = cats.OrderBy(s => s.CatId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Category>.CreateAsync(cats.AsNoTracking(), pageNumber ?? 1, pageSize));
        }



        // GET: Admin/Categories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _repository.Categories
        //        .FirstOrDefaultAsync(m => m.CatId == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        [HttpGet]
        [Route("Create")]
        [CustomeAuthen(View = "Create")]
        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [Route("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatId,CatName,Title,Thumb,Published,Ordering,Parents,Levels,Icon,Cover,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertCat(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        [HttpGet]
        [Route("Update/{id}")]
        [CustomeAuthen(View = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _repository.GetCateById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("Edit", category);
        }

        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdateCat(id, category);
                    TempData["SuccessMessage"] = "Your category was successfully updated";
                    return RedirectToAction(nameof(Edit), "Categories");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the category: " + ex.Message);
            }

            // Add the following line to pass the updated category to the view
            ViewBag.Category = category;

            return View("Edit", category);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.DeleteCat(id);
                    return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
            }
            return View(nameof(Index));

        }
    }
}
