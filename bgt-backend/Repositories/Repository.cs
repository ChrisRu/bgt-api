using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace BGTBackend.Repositories
{
    public abstract class Repository<T>
    {
        protected abstract Dictionary<string, string> DataMap { get; }

        protected string GetSelects(string inner = " ") => string.Join(", ", this.DataMap.Select(kv => kv.Key + inner + kv.Value));

        protected string GetUpdates() => this.GetSelects(" = @");

        protected string GetInserts(string tableName)
        {
            var data = this.DataMap.Where(kv => kv.Value != "Id").Where(kv => kv.Key.StartsWith(tableName));
            string insertInto = string.Join(", ", data.Select(kv => kv.Key));
            string values = string.Join(", ", data.Select(kv => "@" + kv.Value));
            return $@"
                INSERT INTO {tableName}({insertInto})
                VALUES({values})
            ";
        }

        protected static T QueryFirstOrDefault(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Querying from database...", sql);

                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected static IEnumerable<T> Query(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Querying from database...", sql);

                return connection.Query<T>(sql, parameters);
            }
        }

        protected static T Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false)
                {
                    throw new Exception("Can't connect to database");
                }

                LogQuery("Executing on database...", sql);

                return connection.ExecuteScalar<T>(sql, parameters);
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
            return new SqlConnection(Startup.ConnectionString);
        }
    }
}