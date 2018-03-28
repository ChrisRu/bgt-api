using System.Collections.Generic;
using System.Linq;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class StatsRepository : Repository<dynamic>
    {
        public override string TableName { get; }

        public override Dictionary<string, string> DataMap { get; }

        public dynamic GetMeasurementTypes()
        {
            return Query(@"
                SELECT
                    COUNT(project_code) as amount,
                    categorie as category
                FROM project
                GROUP BY categorie
                ORDER BY amount DESC
            ");
        }

        public dynamic GetProjectsCount()
        {
            return Query(@"
                SELECT
                    COUNT(project_code) as openAmount
                FROM project
            ");
        }
    }
}