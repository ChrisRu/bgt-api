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

        public Task<User> Get(int userId)
        {
            return QueryFirstOrDefault("SELECT * FROM user WHERE gebruikers_code = @userId", new { userId });
        }

        public Task<User> Get(string username)
        {
            return QueryFirstOrDefault("SELECT * FROM user WHERE gebruikersnaam = @username", new { username });
        }

        public Task<int> Add(User newUser)
        {
            return Execute(@"
                INSERT INTO user(username, password)
                VALUES(@Username, @Password
            ", newUser);
        }
    }
}