using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BGTBackend.Models;
using Dapper;

namespace BGTBackend.Helpers
{
    public class Repository
    {
        protected Task<T> QueryFirstOrDefault<T>(string sql, Dictionary<string, string> match)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return this.QueryFirstOrDefault<T>(sql + DictToSQL(match), match.Values);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        protected Task<T> QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        
        protected Task<IEnumerable<T>> Query<T>(string sql, Dictionary<string, string> match)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return this.Query<T>(sql + DictToSQL(match), match.Values);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected Task<IEnumerable<T>> Query<T>(string sql, object parameters = null)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return connection.QueryAsync<T>(sql, parameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = this.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return connection.ExecuteAsync(sql, parameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private IDbConnection CreateConnection()
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