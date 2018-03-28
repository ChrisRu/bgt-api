using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class FoundationRepository : Repository<Foundation>
    {
        public override string TableName { get; } = "grondslag";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"grondslag.grondslag_code", "Id"},
            {"grondslag.project_code", "ProjectId"},
            {"grondslag.naam_meten", "MeasurementName"},
            {"grondslag.naam_rekenen", "MeasurementCalculation"},
            {"grondslag.begindatum", "StartDate"},
            {"grondslag.einddatum", "EndDate"},
            {"grondslag.uren_meten", "HoursMeasuring"},
            {"grondslag.uren_rekenen", "HoursCalculating"}
        };
    }
}