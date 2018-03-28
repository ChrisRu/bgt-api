using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class PreparationRepository : Repository<Preparation>
    {
        public override string TableName { get; } = "voorbereiding";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"voorbereiding.voorbereiding_code", "Id"},
            {"voorbereiding.project_code", "ProjectId"},
            {"voorbereiding.naam", "Name"},
            {"voorbereiding.uren", "Hours"},
            {"voorbereiding.einddatum", "EndDate"}
        };
    }
}