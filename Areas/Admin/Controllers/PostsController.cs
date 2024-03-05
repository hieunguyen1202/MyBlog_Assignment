using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language;
using MyBlog.Areas.Admin.Data;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Helpers;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Principal;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Security.AccessControl;
using Minio;
using Minio.DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]

    public class PostsController : Controller
    {
        private readonly IPostRespository _repository;
        private readonly ICategoriesRespository _categoriesRepository;
        private readonly IMinioClient _minioClient;

        public PostsController(IPostRespository repository, ICategoriesRespository categoriesRepository, IMinioClient minioClient)
        {
            _repository = repository;
            _categoriesRepository = categoriesRepository;
            _minioClient = minioClient;

		}

        [HttpGet]
        [Route("Manage")]
        [CustomeAuthen(View = "Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Post> posts = _repository.GetPostsList();
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
                posts = posts.Where(s => (s.Title != null && s.Title.Contains(searchString)) ||
                         (s.Contents != null && s.Contents.Contains(searchString)));
            }
            if (!posts.Any())
            {
                ViewData["Result"] = "No records found.";
            }
            switch (sortOrder)
            {
                case "name_desc":
                    posts = posts.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    posts  = posts .OrderBy(s => s.CreateTime);
                    break;
                case "date_desc":
                    posts  = posts .OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    posts  = posts .OrderBy(s => s.PostId);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Post>.CreateAsync(posts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        //GET: Create
        [HttpGet]
        [Route("Create")]
        [CustomeAuthen(View = "Create")]
        public async Task<IActionResult> Create()
        { 
           ViewData["CatId"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");
            return View();
        }
        [HttpGet]
        [Route("Update")]
        [CustomeAuthen(View = "Update")]
        public IActionResult Update()
        {
            return View();
        }

		[HttpPost]
		[Route("Create")]
		public async Task<IActionResult> Create(Post obj)
		{
			try
			{
				obj.CreateTime = DateTime.Now;

                if (Request.Form.Files.Count > 0)
                {
                    var image = Request.Form.Files[0];
                    if (image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var bucketName = "myblog";
                        var objectName = fileName;
                        var contentType = image.ContentType;

                        using (var stream = image.OpenReadStream())
                        {
                            var poa = new PutObjectArgs()
                            .WithBucket(bucketName)
    .WithObject(objectName)
    .WithFileName(fileName)
    .WithContentType(contentType);

                            await _minioClient.PutObjectAsync(poa);
                        }
						obj.Thumb = fileName;
					}
                   
                    }

                    _repository.InsertPost(obj);

                    return RedirectToAction(nameof(Create));

                }catch (Exception ex)
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
        //[CustomeAuthen(View = "Edit")]
        public IActionResult Edit(int id)
        {
            var post = _repository.GetPostById(id);
            ViewData["CatId"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");

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
