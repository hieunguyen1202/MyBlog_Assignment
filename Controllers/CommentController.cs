using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using MyBlog.Data.Repository;
using MyBlog.Models;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace MyBlog.Controllers
{
    [Route("Comment")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int postId)
        {
            postId = 1;
            var commentList = _repository.GetCommentListByPostId(postId);
            ViewData["CommentList"] = commentList;
            return View("Detail","Home");
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
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
                    _repository.createComment(comment);

                    // Return a JSON response indicating success
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                // Return a JSON response indicating failure with an error message
                return Json(new { success = false, message = "An error occurred while posting the comment: " + ex.Message });
            }

            // Return a JSON response indicating failure if ModelState is not valid
            return Json(new { success = false, message = "Invalid ModelState" });
        }
    }
}

