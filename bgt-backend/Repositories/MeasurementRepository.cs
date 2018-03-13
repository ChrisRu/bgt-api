using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementRepository : Repository<Measurement>
    {
        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            { "meting_code", "Id" },
            { "project_code", "ProjectId" },
            { "oplevering", "DeliveryDate" },
            { "bedrijf", "Company" },
            { "begindatum", "StartDate" },
            { "einddatum_minicomp", "EndDate" }
        };

        public IEnumerable<Measurement> GetAll()
        {
            return Query($@"
                SELECT {this.GetSelects()}
                FROM meting
            ");
        }

        public Measurement Get(int measurementId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM meting
                WHERE meting_code = @measurementId
            ", new { measurementId });
        }

        public Measurement Add(Measurement measurement)
        {
            return Execute(this.GetInserts("meting"), measurement);
        }

        public Measurement Edit(Measurement measurement)
        {
            return Execute($@"
                UPDATE meting
                SET {this.GetUpdates()}
                WHERE meting_code = @Id
            ", measurement);
        }
    }
}