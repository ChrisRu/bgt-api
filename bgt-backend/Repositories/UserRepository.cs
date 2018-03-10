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

        public Task<User> Get(Dictionary<string, string> match)
        {
            return QueryFirstOrDefault("SELECT * FROM user", match);
        }

        public Task<int> Add(User newUser)
        {
            return Execute("INSERT INTO user(username, password) VALUES(@Username, @Password", newUser);
        }
    }
}