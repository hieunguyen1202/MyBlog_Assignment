using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Data.Repository;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
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
        public ActionResult Create(Comment comment)
        {
                if (ModelState.IsValid)
                {
                    _repository.createComment(comment);
                    return RedirectToAction(nameof(Index));
                }
                return View(comment);
            }
        }

    }

