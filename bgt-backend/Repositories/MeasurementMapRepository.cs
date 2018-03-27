using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementMapRepository : Repository<MeasurementMap>
    {
        protected override string TableName { get; } = "meetmap";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"meetmap_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam", "Name"},
            {"einddatum", "EndDate"},
            {"uren", "Hours"},
            {"geschatte_te_meten", "Estimate"}
        };
    }
}