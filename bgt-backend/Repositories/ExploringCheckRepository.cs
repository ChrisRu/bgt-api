using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ExploringCheckRepository : Repository<ExploringCheck>
    {
        protected override string TableName { get; } = "controle_verkenning";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"controle_verkenning_code", "Id"},
            {"verken_code", "ExploringId"},
            {"einddatum", "EndDate"},
            {"uren", "Hours"},
            {"naam", "Name"}
        };
    }
}