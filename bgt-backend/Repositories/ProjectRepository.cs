using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class ProjectRepository : Repository<Project>
    {
        public Task<IEnumerable<Project>> GetAll()
        {
            return Query("SELECT * FROM project");
        }

        public Task<Project> Get(int projectId)
        {
            return QueryFirstOrDefault("SELECT * FROM project WHERE project_code = @projectId", new { projectId });
        }

        public Task<int> Add(Project project)
        {
            return Execute(@"
                INSERT INTO project(bgton_nummer, status, omschrijving, categorie, locatie_code)
                VALUES(@BGTonNumber, @Status, @Description, @Category, @Location)
            ", project);
        }

        public Task<int> Edit(Project project)
        {
            return Execute(@"
                UPDATE project
                SET bgton_nummer = @BGTonNumber, status = @Status, omschrijving = @Description, categorie = @Category, locatie_code = @Location
                WHERE project_code = @projectId
            ", project);
        }
    }
}