using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller<User>
    {
        protected override Repository<User> _repo { get; set; } = new UserRepository();

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response> Edit(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id) throw new Exception("URL klopt niet met de data");

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                return new Response(this.Response, this._repo.Edit(user));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, "Kan gebruiker niet aanpassen: " + error.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<Response> Create([FromBody] User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                return new Response(this.Response, this._repo.Add(user));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, "Kan niet een nieuwe gebruiker aanmaken: " + error.Message));
            }
        }
    }
}