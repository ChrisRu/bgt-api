using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace BGTBackend.Repositories
{
    public abstract class Repository<T>
    {
        protected static Task<T> QueryFirstOrDefault(string sql, Dictionary<string, string> match)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return QueryFirstOrDefault(sql + DictToSQL(match), match.Values);
            }
        }

        protected static Task<T> QueryFirstOrDefault(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        protected static Task<IEnumerable<T>> Query(string sql, Dictionary<string, string> match)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return Query(sql + DictToSQL(match), match.Values);
            }
        }

        protected static Task<IEnumerable<T>> Query(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.QueryAsync<T>(sql, parameters);
            }
        }

        protected static Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return connection.ExecuteAsync(sql, parameters);
            }
        }

        private static IDbConnection CreateConnection()
        {
            var connection = new SqlConnection("server=tcp:localhost, 3306");
            return connection;
        }

        private static string DictToSQL(Dictionary<string, string> match)
        {
            string conditions = string.Join(" AND ", match.Select((kv, index) => kv.Key + "=$" + (index + 1)));
            return " WHERE " + conditions;
        }
    }
}