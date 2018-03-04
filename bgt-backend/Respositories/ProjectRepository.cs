using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Helpers;
using BGTBackend.Models;

namespace BGTBackend.Clients
{
    internal class ProjectRepository : Repository, IRepository<Project>
    {
        public Task<IEnumerable<Project>> GetAll()
        {
            return Query<Project>("SELECT * FROM project");
        }

        public Task<Project> Get(Dictionary<string, string> match)
        {
            return QueryFirstOrDefault<Project>("SELECT * FROM project", match);
        }
    }
}