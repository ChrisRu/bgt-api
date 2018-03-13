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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<User> Edit(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id)
                {
                    throw new Exception("Id does not match");
                }

                return this._repo.Edit(user);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan gebruiker niet aanpassen");
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<User> Create([FromBody] User user)
        {
            try
            {
                return this._repo.Add(user);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan niet een nieuwe gebruiker aanmaken");
                return null;
            }
        }
    }
}