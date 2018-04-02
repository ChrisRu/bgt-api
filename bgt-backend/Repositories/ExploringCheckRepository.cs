using System.Collections.Generic;
using System.Linq;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ExploringCheckRepository : Repository<ExploringCheck>
    {
        public override string TableName { get; } = "controle_verkenning";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"controle_verkenning.controle_verkenning_code", "Id"},
            {"controle_verkenning.verken_code", "ExploringId"},
            {"controle_verkenning.einddatum", "EndDate"},
            {"controle_verkenning.uren", "Hours"},
            {"controle_verkenning.naam", "Name"}
        };

        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id">ID to find in the database</param>
        /// <returns>The found item from the database</returns>
        public override ExploringCheck Get(int id)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM {this.TableName}
                WHERE verken_code = (
                    SELECT verken_code
                    FROM verkennen
                    WHERE project_code = @id
                )
            ", new {id});
        }

        /// <summary>
        /// Delete item(s) from table with matching IDs
        /// </summary>
        /// <param name="id">ID to remove</param>
        /// <returns>null</returns>
        public override ExploringCheck Delete(int id)
        {
            return Execute($@"
                DELETE FROM {this.TableName}
                WHERE verken_code = (
                    SELECT verken_code
                    FROM verkennen
                    WHERE project_code = @id
                )
            ", new {id});
        }

        /// <summary>
        /// Get valid SQL UPDATE query of the properties
        /// </summary>
        /// <returns>SQL UPDATE Query</returns>
        protected override string GetUpdates()
        {
            Dictionary<string, string> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName))
                .ToDictionary(i => i.Key, i => i.Value);

            return $@"
                UPDATE {this.TableName}
                SET {GetSelects(data, " = @")}
                WHERE verken_code = (
                    SELECT verken_code
                    FROM verkennen
                    WHERE project_code = @id
                )
            ";
        }
    }
}