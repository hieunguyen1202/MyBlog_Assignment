using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Data.Repository;
using MyBlog.Models;
using System;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]
    public class HomeController : Controller
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IPostRespository _repositoryPost;
        private readonly ICommentRepository _repositoryComment;
        private readonly ICategoriesRespository _repositoryCategory;
        public HomeController(IAccountsRepository accountsRepository, ICategoriesRespository repositoryCategory, IPostRespository postRespository, ICommentRepository commentRepository)
        {
            _accountsRepository = accountsRepository;
            _repositoryCategory = repositoryCategory;
            _repositoryPost = postRespository;
            _repositoryComment = commentRepository;
        }
        [HttpGet]
        [Route("Home")]
        [CustomeAuthen(View = "Index")]
        public IActionResult Index()
        {
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            int accountId = Int32.Parse(taikhoanID);

            var countPost = _repositoryPost.GetPostCount();
            var countCat = _repositoryCategory.GetCountCategory();
            var countComment = _repositoryComment.GetCountComment();
            var account = _accountsRepository.GetAccountById(accountId);
            ViewData["Account"] = account;
            ViewBag.countPost = countPost;
            ViewBag.countCat = countCat;
            ViewBag.countComment = countComment;

            return View();
        }

    }
}
