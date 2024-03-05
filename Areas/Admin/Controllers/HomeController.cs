using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Models;
using System;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("/Admin")]
    public class HomeController : Controller
    {
        private readonly IAccountsRepository _accountsRepository;
        public HomeController(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }
        [HttpGet]
        [Route("Home")]
        [CustomeAuthen(View = "Index")]
		public IActionResult Index()
		{
			var taikhoanID = HttpContext.Session.GetString("AccountId");
			int accountId= Int32.Parse(taikhoanID);

			
				var account = _accountsRepository.GetAccountById(accountId);
				ViewData["Account"] = account;
			
			return View();
		}

	}
}
