using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class StereokarterenRepository : Repository<Stereokarteren>
    {
        public override string TableName { get; } = "stereokarteren";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"stereokarteren.stereokarteren_code", "Id"},
            {"stereokarteren.project_code", "ProjectId"},
            {"stereokarteren.naam", "Name"},
            {"stereokarteren.begindatum", "StartDate"},
            {"stereokarteren.einddatum", "EndDate"},
            {"stereokarteren.uren", "Hours"},
            {"stereokarteren.punten", "Points"}
        };
    }
}