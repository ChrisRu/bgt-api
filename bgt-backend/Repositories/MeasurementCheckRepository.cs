using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementCheckRepository : Repository<MeasurementCheck>
    {
        protected override string TableName { get; } = "controle_meting";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>();

        public IEnumerable<MeasurementCheck> GetAll()
        {
            return Query($@"
                SELECT {this.GetSelects()}
                FROM controle_meting
            ");
        }

        public MeasurementCheck Get(int measurementCheckId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM controle_meting
                WHERE controle_meting_code = @measurementCheckId
            ", new {measurementCheckId});
        }

        public MeasurementCheck Add(MeasurementCheck measurementCheck)
        {
            return Execute(this.GetInserts(), measurementCheck);
        }

        public MeasurementCheck Edit(MeasurementCheck measurementCheck)
        {
            return Execute(this.GetUpdates(), measurementCheck);
        }
    }
}