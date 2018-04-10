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
        public abstract string TableName { get; }

        public abstract Dictionary<string, string> DataMap { get; }

        /// <summary>
        /// Get all items from the table
        /// </summary>
        /// <returns>A list of all items from the table</returns>
        public IEnumerable<T> GetAll()
        {
            return Query($@"
                SELECT {this.GetSelects()}
                FROM {this.TableName}
            ");
        }

        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id">ID to find in the database</param>
        /// <returns>The found item from the database</returns>
        public virtual T Get(int id)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM {this.TableName}
                WHERE project_code = @id
            ", new {id});
        }

        /// <summary>
        /// Add an item to the table
        /// </summary>
        /// <param name="item">Item to add to the database</param>
        /// <returns>The created database item</returns>
        public virtual T Add(T item)
        {
            return Execute(this.GetInserts(), item);
        }

        /// <summary>
        /// Update an item in the table
        /// </summary>
        /// <param name="item">Item to update in the database</param>
        /// <returns>The modified database item</returns>
        public virtual T Edit(T item)
        {
            return Execute(this.GetUpdates(), item);
        }

        /// <summary>
        /// Delete item(s) from table with matching IDs
        /// </summary>
        /// <param name="id">ID to remove</param>
        /// <returns>null</returns>
        public virtual T Delete(int id)
        {
            return Execute($@"
                UPDATE {this.TableName}
                SET verwijderd = GETDATE()
                WHERE project_code = @id
            ", new {id});
        }

        /// <summary>
        /// Get valid SQL SELECT query of the properties
        /// </summary>
        /// <returns>SQL SELECT Query</returns>
        protected string GetSelects(string inner = " ")
        {
            return GetSelects(this.DataMap, inner);
        }

        /// <summary>
        /// Get valid SQL UPDATE query of the properties
        /// </summary>
        /// <returns>SQL UPDATE Query</returns>
        protected virtual string GetUpdates()
        {
            Dictionary<string, string> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName))
                .ToDictionary(i => i.Key, i => i.Value);

            return $@"
                UPDATE {this.TableName}
                SET {GetSelects(data, " = @")}
                WHERE project_code = @projectId
            ";
        }

        /// <summary>
        /// Get valid SQL INSERT query of the properties
        /// </summary>
        /// <returns>SQL INSERT Query</returns>
        protected string GetInserts()
        {
            IEnumerable<KeyValuePair<string, string>> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName));

            string insertInto = string.Join(", ", data.Select(kv => kv.Key));
            string values = string.Join(", ",
                data.Select(kv => "@" + char.ToLower(kv.Value[0]) + kv.Value.Substring(1)));

            return $@"
                INSERT INTO {this.TableName}({insertInto})
                VALUES({values})
            ";
        }

        /// <summary>
        /// Query from the database and get the first result
        /// </summary>
        /// <param name="sql">Query</param>
        /// <param name="parameters">Optional data</param>
        /// <returns>An item</returns>
        /// <exception cref="Exception">Error when connection to the database has failed</exception>
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

        /// <summary>
        /// Query from the database
        /// </summary>
        /// <param name="sql">Query</param>
        /// <param name="parameters">Optional data</param>
        /// <returns>A list of items</returns>
        /// <exception cref="Exception">Error when connection to the database has failed</exception>
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

        /// <summary>
        /// Execute query on database
        /// </summary>
        /// <param name="sql">Query</param>
        /// <param name="parameters">Optional data</param>
        /// <returns>The item</returns>
        /// <exception cref="Exception">Error when connection to the database has failed</exception>
        protected static T Execute(string sql, object parameters = null)
        {
            using (IDbConnection connection = CreateConnection())
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

        /// <summary>
        /// Get the joined properties of a table
        /// </summary>
        /// <param name="data">Mapped properties</param>
        /// <param name="inner">String to connect the keys and values with</param>
        /// <returns>A string of joined properties in SQL format</returns>
        protected static string GetSelects(Dictionary<string, string> data, string inner = " ")
        {
            return string.Join(", ", data.Select(kv => kv.Key + inner + (
                                                           inner.Contains("@")
                                                               ? char.ToLower(kv.Value[0]) + kv.Value.Substring(1)
                                                               : kv.Value)
            ));
        }

        /// <summary>
        /// Log the query to the console
        /// </summary>
        /// <param name="message">Title of query</param>
        /// <param name="sql">Query itsself</param>
        private static void LogQuery(string message, string sql)
        {
            Console.WriteLine(message, sql);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t" + sql);
            Console.ResetColor();
        }

        /// <summary>
        /// Open and log the database connection and throw errors if any
        /// </summary>
        /// <param name="connection">Connection to connect to</param>
        /// <returns>Connection has opened successfully</returns>
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

        /// <summary>
        /// Create a new connection to the database
        /// </summary>
        /// <returns>The connection</returns>
        private static IDbConnection CreateConnection()
        {
            return new SqlConnection(Startup.ConnectionString);
        }
    }
}