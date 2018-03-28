using System.Linq;
using BGTBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        public Response Authenticated()
        {
            return new Response(this.Response, new
            {
                authenticated = this.User.Identity.IsAuthenticated
            });
        }

        public static string GetCurrentUsername(HttpContext context)
        {
            return context.User.Claims
                .First(e => e.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Value;
        }
    }
}