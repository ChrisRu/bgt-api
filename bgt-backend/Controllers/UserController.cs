using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserRepository _repo = new UserRepository();

        [HttpPost]
        public async Task<User> CreateUser()
        {
            var newUser = new User();
            await this._repo.Add(newUser);
            return newUser;
        }
    }
}