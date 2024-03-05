using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Models
{
    public class CustomeAuthenAttribute : Attribute, IAuthorizationFilter
    {
        public string View { get; set; }
        public string Master { get; set; }

        public CustomeAuthenAttribute()
        {
            View = "error";
            Master = string.Empty;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            CheckIfUserIsAuthenticated(filterContext);
        }

        private void CheckIfUserIsAuthenticated(AuthorizationFilterContext filterContext)
        {
            if (filterContext.Result == null && string.IsNullOrEmpty(filterContext.HttpContext.Session.GetString("AccountId")))
            {
                var returnUrl = filterContext.HttpContext.Request.Path;
                var loginUrl = "/Admin/dang-nhap.html";

                filterContext.Result = new RedirectResult(loginUrl + "?ReturnUrl=" + returnUrl);
            }
        }

    }
}