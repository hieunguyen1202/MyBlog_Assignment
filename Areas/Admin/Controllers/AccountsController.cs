using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Models;

namespace MyBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    [Route("/Admin")]
    public class AccountsController : Controller
    {
        private readonly IAccountsRepository _repository;

        public AccountsController(IAccountsRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin/Accounts
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Logout", Name = "Logout")]
        public IActionResult Logout()
        {
            // Clear the session and any other user-related data
            HttpContext.Session.Clear();

            // Perform any additional logout logic if needed

            return RedirectToAction("Login"); // Redirect the user to the login page or any other page as desired
        }
        [HttpGet]
        [Route("dang-nhap.html", Name = "Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("dang-nhap.html", Name = "Login")]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                var errorMessage = "Login Failed: Email or Password is empty";
                return Json(new { success = false, message = errorMessage });
            }

            var account = _repository.GetAccountByEmail(email);
            if (account == null || !account.Password.Equals(password))
            {
                var errorMessage = "Login Failed: Email or Password is Invalid";
                return Json(new { success = false, message = errorMessage });
            }

            var taikhoanID = HttpContext.Session.GetString("AccountId");
            HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
            HttpContext.Session.SetString("RoleId", account.RoleId.ToString());
            return Json(new { success = true });
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = 11;
            //await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,FullName,Email,Password,Phone,RoleId")] Account account)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(account);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = 11;
            //await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,FullName,Email,Password,Phone,RoleId")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(account);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!AccountExists(account.AccountId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = 11;
            // await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var account = await _context.Accounts.FindAsync(id);
            //_context.Accounts.Remove(account);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
