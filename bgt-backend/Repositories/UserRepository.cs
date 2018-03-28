using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class UserRepository : Repository<User>
    {
        public override string TableName { get; } = "gebruiker";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"gebruiker.gebruiker_code", "Id"},
            {"gebruiker.gebruikersnaam", "Username"},
            {"gebruiker.wachtwoord", "Password"},
            {"gebruiker.admin", "IsAdmin"}
        };

        public User Get(string username)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM gebruiker
                WHERE gebruikersnaam = @username
            ", new {username});
        }

        public User Add(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return Execute(this.GetInserts(), user);
        }

        public User Edit(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return Execute($@"
                UPDATE gebruiker
                SET {this.GetUpdates()}
                WHERE gebruiker_code = @Id
            ", user);
        }
    }
}