using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Models;
using System.Linq;

namespace MyBlog.Controllers
{
    [Route("home")]
    public class PostsController : Controller
    {
        private readonly IPostRespository _repository;

        public PostsController(IPostRespository repository)
        {
            _repository = repository;
        }
        [Route("recent")]
        public IActionResult Index()
        {
            var recentPost = _repository.GetLastPost();
            var newPosts = _repository.GetNewFeedPosts(5);

            ViewBag.ListTitle = "Recent Posts";
            return PartialView("RecentPostPar",
                new PostDetailViewModel
                {
                    Post = recentPost
                }); 
        }
        [Route("view")]
        public IActionResult HotPost()
        {
            ViewBag.ListTitle = "Hot";
            var hotPosts = _repository.GetHotPosts(5);

            return PartialView("HotPostPar", hotPosts);
        }       
        [Route("newfeed")]
        public IActionResult NewFeed()
        {
            ViewBag.ListTitle = "New Feed";
            var newPosts = _repository.GetNewFeedPosts(5);

            return PartialView("NewFeedPar", newPosts);
        }
    }
}
