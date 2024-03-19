using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlog.Areas.Admin.Controllers;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Data.Repository;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoriesRepository _repository;
        private readonly IPostRespository _repositoryPost;
        private readonly ICommentRepository _repositoryComment;
        public HomeController(ICategoriesRepository repository, ILogger<HomeController> logger, IPostRespository repositoryPost, ICommentRepository repositoryComment)
        {

            _repository = repository;
            _repositoryPost = repositoryPost;
            _logger = logger;
            _repositoryComment = repositoryComment;
        }
        [HttpGet]
        [Route("/home", Name = "Home")]
        public IActionResult Index()
        {
            var postList = _repositoryPost.GetPostsList();
            ViewBag.catList = _repository.GetCatList();

            return View();
        }
        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail()
        {
            ViewBag.catList = _repository.GetCatList();
            return View();
        }
        // Controller action
        [HttpGet]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.catList = _repository.GetCatList();
            var commentList = _repositoryComment.GetCommentListByPostId(id);
            var currentPost = _repositoryPost.GetPostById(id);
            var url = await MinIO.GetMinIO(currentPost.Thumb);
            currentPost.Thumb = url;
            //var recentPost = _repositoryPost.GetRecentPosts(1).FirstOrDefault();
            if (currentPost == null)
            {
                return NotFound();
            }

            ViewBag.postId = id;
            //ViewBag.RecentPost = recentPost;
            return View(new PostDetailViewModel
            {
                Post = currentPost,
                CommentList = commentList.ToList(),
                NewComment = new MyBlog.Models.Comment()
            });
        }

        [HttpPost]
        [Route("/create", Name = "Create")]
        public IActionResult Create(Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int count = _repositoryComment.GetCommentByEmailAndPost(comment.Email, (int)comment.PostId);
                    if (count >= 3)
                    {
                        return Json(new { success = false, message = "Cannot comment more than 3 times per one post" });
                    }
                    comment.CreateAt = DateTime.Now;
                    comment.Published = true;
                    _repositoryComment.createComment(comment);
                    // If everything is successful, return JSON indicating success
                    return Json(new { success = true, postId = comment.PostId });
                }
                else
                {
                    // Handle invalid model state, perhaps return validation errors
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                // Return a JSON response indicating failure with an error message
                return Json(new { success = false, message = "An error occurred while posting the comment: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> Search(string search, int? pageNumber)
        {
            ViewBag.catList = _repository.GetCatList();

            var searchResult = _repositoryPost.SearchPost(search);
            foreach (var searchP in searchResult)
            {
                var url = await MinIO.GetMinIO(searchP.Thumb);
                searchP.Thumb = url;
            }

            // ViewBag.Total should be set before paging
            ViewBag.Total = searchResult.Count();

            int pageSize = 4;

            var paginatedSearchResult = await PaginatedList<Post>.CreateAsync(searchResult.AsQueryable(), pageNumber ?? 1, pageSize);

            var postDetailViewModel = new PostDetailViewModel
            {
                Post = null, 
                Category = null,
                CurrentPost = paginatedSearchResult, 
                CommentList = null, 
                NewComment = null,
            };

            return View("Search", postDetailViewModel); 
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
