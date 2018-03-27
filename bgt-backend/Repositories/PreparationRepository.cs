using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class PreparationRepository : Repository<Preparation>
    {
        protected override string TableName { get; } = "voorbereiding";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"voorbereiding_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam", "Name"},
            {"uren", "Hours"},
            {"einddatum_minicomp", "EndDate"}
        };
    }
}