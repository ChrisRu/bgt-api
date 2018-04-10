using System.Collections.Generic;
using System.Linq;
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

        public User GetByUsername(string username)
        {
            return QueryFirstOrDefault($@"
                SELECT
                    gebruiker.gebruiker_code Id,
                    gebruiker.gebruikersnaam Username,
                    gebruiker.wachtwoord Password,
                    gebruiker.admin IsAdmin
                FROM gebruiker
                WHERE gebruikersnaam = @username
            ", new {username});
        }

        public override User Add(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return Execute(this.GetInserts(), user);
        }

        public override User Edit(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            Dictionary<string, string> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName))
                .ToDictionary(i => i.Key, i => i.Value);

            return QueryFirstOrDefault($@"
                UPDATE {this.TableName}
                SET {GetSelects(data, " = @")}
                WHERE gebruiker_code = @Id
            ", user);
        }
    }
}