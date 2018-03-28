using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class ProcessingRepository : Repository<Processing>
    {
        public override string TableName { get; } = "verwerken";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"verwerken.verwerk_code", "Id"},
            {"verwerken.project_code", "ProjectId"},
            {"verwerken.naam", "Name"},
            {"verwerken.begindatum", "StartDate"},
            {"verwerken.einddatum", "EndDate"},
            {"verwerken.uren", "Hours"}
        };
    }
}