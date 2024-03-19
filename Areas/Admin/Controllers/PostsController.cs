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
using Microsoft.Extensions.Configuration;
using Minio.Exceptions;
using Microsoft.CodeAnalysis;
using System.Net.Mime;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]

    public class PostsController : Controller
    {
        private readonly IPostRespository _repository;
        private readonly ICategoriesRespository _categoriesRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly MinioClient _client;
        private readonly IConfiguration _configuration;
        string _bucketName = string.Empty;

        public PostsController(IPostRespository repository, ICategoriesRespository categoriesRepository, MinioClient minioClient, IAccountsRepository accountsRepository, IConfiguration configuration)
        {
            _repository = repository;
            _categoriesRepository = categoriesRepository;
            _client = minioClient;
            _accountsRepository = accountsRepository;
            _configuration = configuration;
            _bucketName = configuration["MinIO:Bucket"];
        }

        [HttpGet]
        [Route("Manage")]
        [CustomeAuthen(View = "Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Post> posts = _repository.GetPostsList();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            int accountId = Int32.Parse(taikhoanID);


            var account = _accountsRepository.GetAccountById(accountId);
            ViewData["Account"] = account;
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
                         (s.Contents != null && s.Contents.Contains(searchString)) || (s.PostId != null && s.PostId.ToString().Contains(searchString)));
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
                case "date":
                    posts = posts.OrderBy(s => s.CreateTime);
                    break;
                case "date_desc":
                    posts = posts.OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    posts = posts.OrderBy(s => s.PostId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Post>.CreateAsync(posts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        //GET: Create
        [HttpGet]
        [Route("Create")]
        //[CustomeAuthen(View = "Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["CatId"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");
            return View();
        }
        [HttpGet]
        [Route("Update")]
        //[CustomeAuthen(View = "Update")]
        public IActionResult Update()
        {
            ViewData["catList"] = new SelectList(_categoriesRepository.GetCatList(), "CatId", "CatName");

            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Post obj, IFormFile Thumb)
        {
            try
            {
                obj.CreateTime = DateTime.Now;

                // Check if the bucket exists, create it if it doesn't
                var bktExistArgs = new BucketExistsArgs().WithBucket(_bucketName);
                var found = await _client.BucketExistsAsync(bktExistArgs).ConfigureAwait(false);

                if (!found)
                {
                    var mkBktArgs = new MakeBucketArgs().WithBucket(_bucketName);
                    await _client.MakeBucketAsync(mkBktArgs).ConfigureAwait(false);
                }

                // Check if the uploaded file is not null
                if (Thumb != null)
                {
                    // Check if the uploaded file is an image
                    if (IsImageFile(Thumb))
                    {
                        var objectName = $"{Thumb.FileName}";
                        await MinIO.PutMinIO(Thumb);
                        obj.Thumb = objectName;
                        ViewBag.ErrorMessage = "Successfully uploaded " + objectName;
                    }
                    else
                    {
                        // Handle the case where the uploaded file is not an image
                        var errorMessage = "Please upload a valid image file.";
                        return Json(new { success = false, message = errorMessage });
                    }
                }
                else
                {
                    // Handle the case where the uploaded file is null or undefined
                    var errorMessage = "Please upload an image file.";
                    return Json(new { success = false, message = errorMessage });
                }

                _repository.InsertPost(obj);
                return Json(new { success = true }); // Return success response
            }
            catch (MinioException ex)
            {
                ViewBag.ErrorMessage = DateTime.Now + " An error occurred while creating the post: " + ex.Message;
                return Json(new { success = false, message = ex.Message }); // Return error response
            }
        }



        private bool IsImageFile(IFormFile file)
        {
            // Validate if the file is an image
            if (file.ContentType.ToLower() == "image/jpeg" ||
                file.ContentType.ToLower() == "image/png" ||
                file.ContentType.ToLower() == "image/gif" ||
                file.ContentType.ToLower() == "image/jpg")
            {
                return true;
            }
            return false;
        }
        MyBlogContext myBlogContext = new MyBlogContext();
        [HttpGet]
        [Route("Update/{id}")]
        [CustomeAuthen(View = "Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var post = _repository.GetPostById(id);
            var categories = myBlogContext.Categories.ToList();

            if (post == null)
            {
                return NotFound();
            }
            ViewBag.CatId = post.CatId;
            ViewBag.Categories = categories;

            return View("Edit", post);
        }


        [HttpPost]
        [Route("Update/{id}")]
        public async Task<IActionResult> Edit(int id, Post post, IFormFile Thumb)
        {
            post.Thumb = null;
            var thumb = myBlogContext.Posts.Where(p => p.PostId == id).FirstOrDefault();
            try
            {
                if (Thumb != null && Thumb.Length > 0) // Check if a new file is uploaded and it has content
                {
                    if (IsImageFile(Thumb))
                    {
                        var objectName = $"{Thumb.FileName}";
                        await MinIO.PutMinIO(Thumb);
                        post.Thumb = objectName;
                        ViewBag.ErrorMessage = "Successfully uploaded " + objectName;
                    }
                    else
                    {
                        // Handle invalid file type error
                        ModelState.AddModelError("", "Invalid file type. Please upload an image file.");
                        return View("Edit", post);
                    }
                }
                else
                {
                    // If no new file is uploaded, retain the existing Thumb value
                    post.Thumb = thumb.Thumb;
                }

                _repository.UpdatePost(id, post);
                TempData["SuccessMessage"] = "Your post was successfully updated";
                return RedirectToAction("Manage", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the post: " + ex.Message);
            }

            return View("Edit", post);
        }



        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
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

    }
}
