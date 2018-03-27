using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementRepository : Repository<Measurement>
    {
        protected override string TableName { get; } = "meting";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"meting_code", "Id"},
            {"project_code", "ProjectId"},
            {"oplevering", "DeliveryDate"},
            {"bedrijf", "Company"},
            {"begindatum", "StartDate"},
            {"einddatum_minicomp", "EndDate"}
        };
    }
}