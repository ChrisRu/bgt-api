using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ExploringRepository : Repository<Exploring>
    {
        public override string TableName { get; } = "verkennen";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"verkennen.verken_code", "Id"},
            {"verkennen.project_code", "ProjectId"},
            {"verkennen.m2_code", "M2"},
            {"verkennen.naam", "Name"},
            {"verkennen.begindatum", "StartDate"},
            {"verkennen.einddatum", "EndDate"},
            {"verkennen.einddatum_opgeleverd", "EndDateDelivered"},
            {"verkennen.opmerking", "Remarks"}
        };
    }
}