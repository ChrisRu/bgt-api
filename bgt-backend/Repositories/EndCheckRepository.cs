using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class EndCheckRepository : Repository<EndCheck>
    {
        public override string TableName { get; } = "eind_controle";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"eind_controle.eind_controle_code", "Id"},
            {"eind_controle.project_code", "ProjectId"},
            {"eind_controle.naam", "Name"},
            {"eind_controle.uren", "Hours"},
            {"eind_controle.einddatum_minicomp", "EndDate"}
        };
    }
}