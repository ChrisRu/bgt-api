using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementMapRepository : Repository<MeasurementMap>
    {
        public override string TableName { get; } = "meetmap";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"meetmap.meetmap_code", "Id"},
            {"meetmap.project_code", "ProjectId"},
            {"meetmap.naam", "Name"},
            {"meetmap.einddatum", "EndDate"},
            {"meetmap.uren", "Hours"},
            {"meetmap.geschatte_te_meten", "Estimate"}
        };
    }
}