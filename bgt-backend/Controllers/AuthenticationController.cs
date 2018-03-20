using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/authenticated")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        [Authorize]
        public object Get()
        {
            return new
            {
                authorized = this.User.Identity.IsAuthenticated
            };
        }

        public static string GetCurrentUsername(HttpContext context)
        {
            return context.User.Claims
                .First(e => e.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Value;
        }
    }
}