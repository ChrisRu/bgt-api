using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Repositories;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class ProjectRepository : Repository<Project>
    {
        public Task<IEnumerable<Project>> GetAll()
        {
            return Query("SELECT * FROM project");
        }

        public Task<Project> Get(Dictionary<string, string> match)
        {
            return QueryFirstOrDefault("SELECT * FROM project", match);
        }
    }
}