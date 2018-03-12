using System;
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
        protected static Task<T> QueryFirstOrDefault(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                Console.WriteLine("Querying from database...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t" + sql);
                Console.ResetColor();

                return connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        protected static Task<IEnumerable<T>> Query(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                Console.WriteLine("Querying from database...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t" + sql);
                Console.ResetColor();

                return connection.QueryAsync<T>(sql, parameters);
            }
        }

        protected static Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                Console.WriteLine("Executing on database...");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t" + sql);
                Console.ResetColor();

                return connection.ExecuteAsync(sql, parameters);
            }
        }

        private static IDbConnection CreateConnection()
        {
            try
            {
                Console.WriteLine("Connecting to the database...");

                var connection = new SqlConnection("server=tcp:localhost, 3306");
                return connection;
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to connect to the database: " + error.Message);
                Console.ResetColor();

                return CreateConnection();
            }
        }
    }
}