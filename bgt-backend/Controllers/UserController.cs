using System;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserRepository _repo = new UserRepository();

        [HttpPost]
        [Authorize]
        public async Task<User> CreateUser([FromBody] User user)
        {
            try
            {
                return await this._repo.Add(user);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan niet een nieuwe gebruiker aanmaken");
                return null;
            }
        }
    }
}