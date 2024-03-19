using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using NuGet.Protocol.Core.Types;
using System.Linq;
using System;
using System.Threading.Tasks;
using MyBlog.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin/Comment")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;
        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("Manage")]
        [CustomeAuthen(View = "Index")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            IQueryable<Comment> comment = _repository.GetCommentsList();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                comment = comment.Where(s => (s.Email != null && s.Email.Contains(searchString)) ||
                         (s.Content != null && s.Content.Contains(searchString)));
            }
            if (!comment.Any())
            {
                ViewData["Result"] = "No records found.";
            }
            switch (sortOrder)
            {
                case "name_desc":
                    comment = comment.OrderByDescending(s => s.Email);
                    break;
                case "Date":
                    comment = comment.OrderBy(s => s.Content);
                    break;
                case "date_desc":
                    comment = comment.OrderByDescending(s => s.Content);
                    break;
                default:
                    comment = comment.OrderBy(s => s.PostId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Comment>.CreateAsync(comment.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.DeleteComment(id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the comment: " + ex.Message;
            }
            return View(nameof(Index));
        }
    }

}
