using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ExploringCheckRepository : Repository<ExploringCheck>
    {
        public override string TableName { get; } = "controle_verkenning";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"controle_verkenning.controle_verkenning_code", "Id"},
            {"controle_verkenning.verken_code", "ExploringId"},
            {"controle_verkenning.einddatum", "EndDate"},
            {"controle_verkenning.uren", "Hours"},
            {"controle_verkenning.naam", "Name"}
        };
    }
}