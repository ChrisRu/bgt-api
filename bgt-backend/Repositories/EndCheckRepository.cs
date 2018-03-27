using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class EndCheckRepository : Repository<EndCheck>
    {
        protected override string TableName { get; } = "eind_controle";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"eind_controle_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam", "Name"},
            {"uren", "Hours"},
            {"einddatum_minicomp", "EndDate"}
        };
    }
}