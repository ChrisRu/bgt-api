using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class StereokarterenRepository : Repository<Stereokarteren>
    {
        protected override string TableName { get; } = "stereokarteren";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"stereokarteren_code", "Id"},
            {"project_code", "ProjectId"},
            {"naam", "Name"},
            {"begindatum", "StartDate"},
            {"einddatum", "EndDate"},
            {"uren", "Hours"},
            {"puntent", "Points"}
        };
    }
}