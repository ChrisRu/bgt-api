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

        public Task<int> Add(Measurement measurement)
        {
            return Execute(@"
                
            ", measurement);
        }

        public Task<int> Edit(Measurement measurement)
        {
            return Execute(@"
                
            ", measurement);
        }
    }
}
