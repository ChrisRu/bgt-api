using System.Collections.Generic;
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
            {"controle_meting.punten", "Points"}
        };
    }
}