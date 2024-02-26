using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlog.Areas.Admin.Controllers;
using MyBlog.Areas.Admin.Data.Repository;
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
        private readonly ICategoriesRespository _repository;
        private readonly IPostRespository _repositoryPost;
        public HomeController(ICategoriesRespository repository, ILogger<HomeController> logger, IPostRespository repositoryPost)
        {
            _repository = repository;
            _repositoryPost = repositoryPost;
            _logger = logger;
        }
        [HttpGet]
        [Route("/home", Name ="Home")]
        public IActionResult Index()
        {
            //var postList = _repositoryPost.GetPostsList();
            var catList = _repository.GetCatList();

            //var viewModel = new IndexViewModel
            //{
            //    Categories = catList.ToList(),
            //    Posts = postList.ToList()
            //};

            return View(catList);
        }
        [HttpGet]
        [Route("Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var post = _repositoryPost.GetPostById(id);
			//var catList = _repository.GetCatList();
			//var viewModel = new IndexViewModel
			//{
			//    Categories = catList.ToList(),
			//    Posts = postList.ToList()
			//};
			if (post == null)
			{
				return NotFound();
			}
			return View(post);
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
