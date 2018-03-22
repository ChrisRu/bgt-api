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
        protected abstract string TableName { get; }

        protected abstract Dictionary<string, string> DataMap { get; }

        private static string GetSelects(Dictionary<string, string> data, string inner = " ")
        {
            return string.Join(", ", data.Select(kv => kv.Key + inner + kv.Value));
        }

        protected string GetSelects(string inner = " ")
        {
            return GetSelects(this.DataMap, inner);
        }

        protected string GetUpdates()
        {
            Dictionary<string, string> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName))
                .ToDictionary(i => i.Key, i => i.Value);

            return $@"
                UPDATE project
                SET {GetSelects(data, " = @")}
                WHERE project_code = @Id
            ";
        }

        protected string GetInserts()
        {
            IEnumerable<KeyValuePair<string, string>> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName));

            string insertInto = string.Join(", ", data.Select(kv => kv.Key));
            string values = string.Join(", ", data.Select(kv => "@" + kv.Value));

            return $@"
                INSERT INTO {this.TableName}({insertInto})
                VALUES({values})
            ";
        }

        protected static T QueryFirstOrDefault(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false) throw new Exception("Can't connect to database");

                LogQuery("Querying from database...", sql);

                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected static IEnumerable<T> Query(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false) throw new Exception("Can't connect to database");

                LogQuery("Querying from database...", sql);

                return connection.Query<T>(sql, parameters);
            }
        }

        protected static T Execute(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                bool connected = Connect(connection);

                if (connected == false) throw new Exception("Can't connect to database");

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