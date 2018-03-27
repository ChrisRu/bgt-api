using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ExploringRepository : Repository<Exploring>
    {
        protected override string TableName { get; } = "verkennen";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"verken_code", "Id"},
            {"project_code", "ProjectId"},
            {"m2_code", "M2"},
            {"naam", "Name"},
            {"begindatum", "StartDate"},
            {"einddatum", "EndDate"},
            {"einddatum_opgeleverd", "EndDateDelivered"},
            {"opmerking", "Remarks"}
        };
    }
}