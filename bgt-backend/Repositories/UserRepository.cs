using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class UserRepository : Repository<User>
    {
        public Task<IEnumerable<User>> GetAll()
        {
            return Query("SELECT * FROM user");
        }

        public async Task<User> Get(Dictionary<string, string> match)
        {
            // TODO: FIX THIS
            return new User{ Password = "123", Username = "test" };
            // return QueryFirstOrDefault<User>("SELECT * FROM user", match);
        }
    }
}