using System.Collections.Generic;
using System.Linq;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementCheckRepository : Repository<MeasurementCheck>
    {
        public override string TableName { get; } = "controle_meting";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"controle_meting.controle_meting_code", "Id"},
            {"controle_meting.meting_code", "MeasurementId"},
            {"controle_meting.naam", "Name"},
            {"controle_meting.einddatum", "EndDate"},
            {"controle_meting.uren", "Hours"},
            {"controle_meting.voorlopig_geleverde_punten", "Points"}
        };

        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id">ID to find in the database</param>
        /// <returns>The found item from the database</returns>
        public override MeasurementCheck Get(int id)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM {this.TableName}
                WHERE meting_code = (
                    SELECT meting_code
                    FROM meting
                    WHERE project_code = @id
                )
            ", new {id});
        }

        /// <summary>
        /// Delete item(s) from table with matching IDs
        /// </summary>
        /// <param name="id">ID to remove</param>
        /// <returns>null</returns>
        public override MeasurementCheck Delete(int id)
        {
            return Execute($@"
                DELETE FROM {this.TableName}
                WHERE meting_code = (
                    SELECT meting_code
                    FROM meting
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
                WHERE meting_code = (
                    SELECT meting_code
                    FROM meting
                    WHERE project_code = @id
                )
            ";
        }
    }
}