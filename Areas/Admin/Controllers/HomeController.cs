using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
