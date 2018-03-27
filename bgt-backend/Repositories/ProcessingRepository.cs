using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ProcessingRepository : Repository<Processing>
    {
        protected override string TableName { get; } = "verwerken";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"verwerk_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam", "Name"},
            {"begindatum", "StartDate"},
            {"einddatum", "EndDate"},
            {"uren", "Hours"}
        };
    }
}