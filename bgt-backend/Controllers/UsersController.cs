using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly UserRepository _repo = new UserRepository();

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response> Edit(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id) throw new Exception("URL klopt niet met de data");

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