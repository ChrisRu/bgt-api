using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class FoundationRepository : Repository<Foundation>
    {
        protected override string TableName { get; } = "grondslag";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"grondslag_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam_meten", "MeasurementName"},
            {"naam_rekenen", "MeasurementCalculation"},
            {"begindatum", "StartDate"},
            {"einddatum", "EndDate"},
            {"uren_meten", "HoursMeasuring"},
            {"uren_rekenen", "HoursCalculating"}
        };
    }
}