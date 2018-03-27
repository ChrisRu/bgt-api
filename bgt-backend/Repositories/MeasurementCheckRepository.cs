using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementCheckRepository : Repository<MeasurementCheck>
    {
        protected override string TableName { get; } = "controle_meting";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>();
    }
}