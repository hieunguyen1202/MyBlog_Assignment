using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using MyBlog.Data.Repository;
using MyBlog.Models;
using System;

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
        public ActionResult Details(int id)
        {
            return View();
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
            try { 
            if (ModelState.IsValid)
            {comment.CreateAt= DateTime.Now;
                comment.Published = true;            
                _repository.createComment(comment);
                return RedirectToAction("Detail","Home");
            }
        }catch (Exception ex)
			{
				ViewBag.ErrorMessage = DateTime.Now + " An error occurred while post comment: " + ex.Message;
			}
            return View("Detail","Home");
        }
    }
}

