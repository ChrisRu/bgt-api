using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    public class MeasurementRepository : Repository<Measurement>
    {
        public Task<IEnumerable<Measurement>> GetAll()
        {
            return Query("SELECT * FROM meting");
        }

        public Task<Measurement> Get(int measurementId)
        {
            return QueryFirstOrDefault("SELECT * FROM meting WHERE meting_code = @measurementId", new { measurementId });
        }

        public Task<Measurement> Add(Measurement measurement)
        {
            return Execute(@"
                INSERT INTO meting(project_code, bedrijf, oplevering, begindatum, einddatum_minicomp)
                VALUES(@ProjectId, @Company, @DeliveryDate, @StartDate, @EndDate)
            ", measurement);
        }

        public Task<Measurement> Edit(Measurement measurement)
        {
            return Execute(@"
                UPDATE meting
                SET project_code = @ProjectId, bedrijf = @Company, oplevering = @DeliveryDate, begindatum = @StartDate, einddatum_minicomp = @EndDate
                WHERE meting_code = @Id
            ", measurement);
        }
    }
}
