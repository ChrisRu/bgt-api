using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/authenticated")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return new
            {
                authorized = this.User.Identity.IsAuthenticated
            };
        }
    }
}