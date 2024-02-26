using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using MyBlog.Areas.Admin.Data;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRespository _repository;
        private readonly ICategoriesRespository _categoriesRepository;

        public PostsController(IPostRespository repository, ICategoriesRespository categoriesRepository)
        {
            _repository = repository;
            _categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        [Route("Manage")]
        public IActionResult Index()
        {
            var postList = _repository.GetPostsList();

            return View(postList);
        }
        // GET: Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");
            return View();
        }
        [HttpGet]
        [Route("Update")]
        public IActionResult Update()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Post obj)
        {
            try
            {
                obj.CreateTime = DateTime.Now;

                _repository.InsertPost(obj);
                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = DateTime.Now + " An error occurred while creating the post: " + ex.Message;
            }

            return View(obj);
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.DeletePost(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the post: " + ex.Message;
            }
            return View(nameof(Index));
        }
        [HttpPost]
        [Route("Update/{id}")]
        public IActionResult Update(int id, Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdatePost(id, post);
                    TempData["SuccessMessage"] = "Your post was successfully updated";
                    return RedirectToAction("Manage", "Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the post: " + ex.Message);
            }

            return View("Update", post);
        }

        [HttpGet]
        [Route("Update/{id}")]
        public IActionResult Edit(int id)
        {
            var post = _repository.GetPostById(id);
            //ViewData["CatId"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");

            if (post == null)
            {
                return NotFound();
            }

            return View("Update", post);
        }
        //public IActionResult Update(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = _repository.GetPostById(id.Value);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //[HttpGet]
        //[Route("Manage")]
        //public IActionResult GetAll()
        //{
        //    return View();
        //}
    }
}
