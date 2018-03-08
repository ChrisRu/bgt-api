using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Helpers;
using BGTBackend.Models;

namespace BGTBackend.Clients
{
    internal class UserRepository : Repository
    {
        public Task<IEnumerable<User>> GetAll()
        {
            return Query<User>("SELECT * FROM user");
        }

        public async Task<User> Get(Dictionary<string, string> match)
        {
            // TODO: FIX THIS
            return new User{ Password = "123", Username = "test" };
            // return QueryFirstOrDefault<User>("SELECT * FROM user", match);
        }
    }
}