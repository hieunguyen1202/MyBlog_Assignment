using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var recentPost = _repository.GetLastPost();
            var url = await MinIO.GetMinIO(recentPost.Thumb);
            recentPost.Thumb = url;
            ViewBag.ListTitle = "Bài Viết Gần Đây";
            return PartialView("RecentPostPar",
                new PostDetailViewModel
                {
                    Post = recentPost
                });
        }
        [Route("view")]
        public async Task<IActionResult> HotPost()
        {
            ViewBag.ListTitle = "Hot";
            var hotPosts = _repository.GetHotPosts(5);
            foreach (var hotPost in hotPosts)
            {
                var url = await MinIO.GetMinIO(hotPost.Thumb);
                hotPost.Thumb = url;
            }
            return PartialView("HotPostPar", hotPosts);
        }
        [Route("newfeed")]
        public async Task<IActionResult> NewFeed()
        {
            ViewBag.ListTitle = "Bài Viết Mới";
            var newPosts = _repository.GetNewFeedPosts(3);
            foreach (var newP in newPosts)
            {
                var url = await MinIO.GetMinIO(newP.Thumb);
                newP.Thumb = url;
            }
            return PartialView("NewFeedPar", newPosts);
        }
        [Route("recentPost")]
        public async Task<IActionResult> RecentPost()
        {
            ViewBag.ListTitle = "Bài Viết Gần Đây";
            var recentPosts = _repository.GetRecentPosts(5);
            foreach (var newP in recentPosts)
            {
                var url = await MinIO.GetMinIO(newP.Thumb);
                newP.Thumb = url;
            }
            return PartialView("RecentPostListPar", recentPosts);
        }
        [Route("tagComment")]
        public IActionResult TagComment()
        {
            var recentPosts = _repository.GetTags(10);
            ViewBag.ListTitle = "Tags";

            return PartialView("TagCommentPar", recentPosts);
        }
        [Route("carousel")]
        public async Task<IActionResult> Carousel()
        {
            var recentPosts = _repository.GetTop3Post(5);
            foreach (var newP in recentPosts)
            {
                var url = await MinIO.GetMinIO(newP.Thumb);
                newP.Thumb = url;
            }
            return PartialView("PostCarouselPar", recentPosts);
        }
        [Route("categoryTech")]
        public async Task<IActionResult> CategoryTech()
        {
            var posts = _repository.GetPostByCatId(10, 5);
            ViewBag.ListTitle = "Công Nghệ";
            foreach (var newP in posts)
            {
                var url = await MinIO.GetMinIO(newP.Thumb);
                newP.Thumb = url;
            }
            return PartialView("CatTechPar", posts);
        }

        [Route("category")]
        public async Task<IActionResult> Category()
        {
            var catList = _repository.GetCatIdList();
            var random = new Random();
            var randomCatIds = catList.OrderBy(x => random.Next()).ToList();
            IEnumerable<Post> recentPosts;
            string listTitle;

            switch (randomCatIds[0])
            {
                case 2:
                    listTitle = "Y Tế";
                    recentPosts = _repository.GetPostByCatId(2, 5);
                    foreach (var newP in recentPosts)
                    {
                        var url = await MinIO.GetMinIO(newP.Thumb);
                        newP.Thumb = url;
                    }
                    break;
                case 3:
                    listTitle = "Chính Trị";
                    recentPosts = _repository.GetPostByCatId(3, 5);
                    foreach (var newP in recentPosts)
                    {
                        var url = await MinIO.GetMinIO(newP.Thumb);
                        newP.Thumb = url;
                    }
                    break;
                case 4:
                    listTitle = "Giáo Dục";
                    recentPosts = _repository.GetPostByCatId(4, 5);
                    foreach (var newP in recentPosts)
                    {
                        var url = await MinIO.GetMinIO(newP.Thumb);
                        newP.Thumb = url;
                    }
                    break;

                case 11:
                    listTitle = "Xã Hội";
                    recentPosts = _repository.GetPostByCatId(11, 5);
                    foreach (var newP in recentPosts)
                    {
                        var url = await MinIO.GetMinIO(newP.Thumb);
                        newP.Thumb = url;
                    }
                    break;

                default:
                    listTitle = "Other";
                    recentPosts = _repository.GetTop3Post(5);
                    foreach (var newP in recentPosts)
                    {
                        var url = await MinIO.GetMinIO(newP.Thumb);
                        newP.Thumb = url;
                    }
                    break;
            }

            ViewBag.ListTitle = listTitle;

            return PartialView("CategoryViewPar", recentPosts);
        }
    }
}
