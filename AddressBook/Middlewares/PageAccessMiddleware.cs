﻿using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AddressBook.Middlewares
{
    public class PageAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public PageAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            var isProtectedPage = path != null &&
                                  !path.StartsWith("/api") &&
                                  !path.StartsWith("/auth/login") &&
                                  !path.StartsWith("/auth/register") &&
                                  !path.StartsWith("/auth/verifyemail") && 
                                  !path.StartsWith("/auth/changepassword") &&
                                  !path.Contains(".");

            if (isProtectedPage && !context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Auth/Login");
                return;
            }

            await _next(context);
        }
    }
}
