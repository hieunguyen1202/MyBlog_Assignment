using Microsoft.AspNetCore.Mvc;
using MyBlog.Areas.Admin.Data.Repository;

namespace MyBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRespository _repository;

        public PostsController(IPostRespository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
