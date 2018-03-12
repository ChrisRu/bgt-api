using Microsoft.AspNetCore.Authorization;
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
    }
}