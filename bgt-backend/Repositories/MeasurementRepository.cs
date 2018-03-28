using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementRepository : Repository<Measurement>
    {
        public override string TableName { get; } = "meting";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"meting.meting_code", "Id"},
            {"meting.project_code", "ProjectId"},
            {"meting.oplevering", "DeliveryDate"},
            {"meting.bedrijf", "Company"},
            {"meting.begindatum", "StartDate"},
            {"meting.einddatum_minicomp", "EndDate"}
        };
    }
}