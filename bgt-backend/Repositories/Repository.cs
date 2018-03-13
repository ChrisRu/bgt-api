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
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Querying from database...", sql);

                return connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        protected static Task<IEnumerable<T>> Query(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Querying from database...", sql);

                return connection.QueryAsync<T>(sql, parameters);
            }
        }

        protected static Task<T> Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Executing on database...", sql);

                return connection.ExecuteScalarAsync<T>(sql, parameters);
            }
        }

        private static void LogQuery(string message, string sql)
        {
            Console.WriteLine(message, sql);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t" + sql);
            Console.ResetColor();
        }

        private static bool Connect(IDbConnection connection)
        {
            try
            {
                Console.WriteLine("Opening database connection...");
                connection.Open();
                return true;
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to connect to the database: " + error.Message);
                Console.ResetColor();
                return false;
            }
        }

        private static IDbConnection CreateConnection()
        {
            return new SqlConnection("server=tcp:localhost, 3306");
        }
    }
}