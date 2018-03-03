using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Helpers;
using BGTBackend.Models;

namespace BGTBackend.Clients
{
    public class ProjectRepository : Repository
    {
        public Task<IEnumerable<Project>> GetAll()
        {
            return this.Query<Project>("SELECT * FROM project");
        }

        public Task<Project> Get(Dictionary<string, string> match)
        {
            return this.QueryFirstOrDefault<Project>("SELECT * FROM project", match);
        }
    }
}