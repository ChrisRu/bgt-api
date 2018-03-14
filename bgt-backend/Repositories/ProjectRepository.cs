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
            { "project.project_code", "Id" },
            { "project.bgton_nummer", "BGTonNumber" },
            { "project.status", "Status" },
            { "project.omschrijving", "Description" },
            { "project.categorie", "Category" },
            { "project.locatie_code", "LocationCode" },
            { "project.laatst_aangepast_datum", "LastEditedDate" },
            { "project.laatst_aangepast_gebruiker", "LastEditedUser" },
            { "locatie.longtitude", "Longitude" },
            { "locatie.latitude", "Latitude" },
            { "gebruiker.gebruikersnaam", "Username" }
        };

        public IEnumerable<Project> GetAll()
        {
            Console.WriteLine(this.GetSelects());
            return Query($@"
                SELECT {this.GetSelects()}
                FROM project
                JOIN gebruiker on project.laatst_aangepast_gebruiker = gebruiker.gebruiker_code
                JOIN locatie on project.locatie_code = locatie.locatie_code
            ");
        }

        public Project Get(int projectId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM project
                WHERE project.project_code = @projectId
                JOIN gebruiker on project.laatst_aangepast_gebruiker = gebruiker.gebruiker_code
                JOIN locatie on project.locatie_code = locatie.locatie_code
            ", new { projectId });
        }

        public Project Add(ProjectPost project)
        {
            return Execute(this.GetInserts("project"), project);
        }

        public Project Edit(ProjectPost project)
        {
            return Execute($@"
                UPDATE project
                SET {this.GetUpdates()}
                WHERE project_code = @Id
            ", project);
        }
    }
}