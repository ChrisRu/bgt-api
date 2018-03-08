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
        
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> results = await this._repo.GetAll();
            return results.Select(Filter);
        }

        [HttpGet]
        public async Task<User> Get(Dictionary<string, string> match)
        {
            var result = await this._repo.Get(match);
            return Filter(result);
        }

        private static User Filter(User user)
        {
            user.Password = null;
            return user;
        }
    }
}