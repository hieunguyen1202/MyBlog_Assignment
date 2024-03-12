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
        [Route("/home", Name ="Home")]
        public IActionResult Index()
        {
            var postList = _repositoryPost.GetPostsList();
            ViewBag.catList = _repository.GetCatList();
            //var catList = _repository.GetCatList();
            //var viewModel = new IndexViewModel
            //{
            //   Categories = catList.ToList(),
            //   Posts = postList.ToList()
            //};
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
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public ActionResult Create(Comment comment)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    comment.CreateAt = DateTime.Now;
                    comment.Published = true;
                    _repositoryComment.createComment(comment);
                }
                return RedirectToAction("Detail", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                // Return a JSON response indicating failure with an error message
                return Json(new { success = false, message = "An error occurred while posting the comment: " + ex.Message });
            }
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
