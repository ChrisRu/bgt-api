using System.Collections.Generic;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class ProjectRepository : Repository<Project>
    {
        protected override string TableName { get; } = "project";

        protected override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"project.project_code", "Id"},
            {"project.bgton_nummer", "BGTonNumber"},
            {"project.status", "Status"},
            {"project.omschrijving", "Description"},
            {"project.categorie", "Category"},
            {"project.locatie_code", "LocationCode"},
            {"project.laatst_aangepast_datum", "LastEditedDate"},
            {"project.laatst_aangepast_gebruiker", "LastEditedUser"},
            {"locatie.longtitude", "Longtitude"},
            {"locatie.latitude", "Latitude"},
            {"gebruiker.gebruikersnaam", "Username"}
        };

        public IEnumerable<Project> GetAll()
        {
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
            ", new {projectId});
        }

        public Project Add(ProjectPost project)
        {
            return Execute(this.GetInserts(), project);
        }

        public Project Edit(ProjectPost project)
        {
            return Execute(this.GetUpdates(), project);
        }
    }
}