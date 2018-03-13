using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class ProjectRepository : Repository<Project>
    {
        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            { "project_code", "Id" },
            { "bgton_nummer", "BGTonNumber" },
            { "status", "Status" },
            { "omschrijving", "Description" },
            { "categorie", "Category" },
            { "locatie_code", "Location" },
            { "laatst_aangepast_datum", "LastEditedDate" },
            { "laatst_aangepast_gebruiker", "LastEditedUser" }
        };

        public IEnumerable<Project> GetAll()
        {
            Console.WriteLine(this.GetSelects());
            return Query($@"
                SELECT {this.GetSelects()}
                FROM project
            ");
        }

        public Project Get(int projectId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM project
                WHERE project_code = @projectId
            ", new { projectId });
        }

        public Project Add(Project project)
        {
            return Execute(this.GetInserts("project"), project);
        }

        public Project Edit(Project project)
        {
            return Execute($@"
                UPDATE project
                SET {this.GetUpdates()}
                WHERE project_code = @Id
            ", project);
        }
    }
}