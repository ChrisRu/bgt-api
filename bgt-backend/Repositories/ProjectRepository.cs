using System.Collections.Generic;
using System.Linq;
using BGTBackend.Models;

namespace BGTBackend.Repositories
{
    internal class ProjectRepository : Repository<Project>
    {
        public override string TableName { get; } = "project";

        public override Dictionary<string, string> DataMap { get; } = new Dictionary<string, string>
        {
            {"project.project_code", "Id"},
            {"project.bgton_nummer", "BgTonNumber"},
            {"project.status", "Status"},
            {"project.omschrijving", "Description"},
            {"project.categorie", "Category"},
            {"project.locatie_code", "LocationCode"},
            {"project.laatst_aangepast_datum", "LastEditedDate"},
            {"project.laatst_aangepast_gebruiker", "LastEditedUser"},
            {"locatie.longtitude", "Longtitude"},
            {"locatie.latitude", "Latitude"},
            {"gebruiker.gebruikersnaam", "Username"},
            {"verkennen.begindatum", "ExploreDate"}
        };

        public IEnumerable<Project> GetAll()
        {
            return Query($@"
                SELECT {this.GetSelects()}
                FROM project
                JOIN gebruiker ON project.laatst_aangepast_gebruiker = gebruiker.gebruiker_code
                JOIN locatie ON project.locatie_code = locatie.locatie_code
                LEFT OUTER JOIN verkennen ON verkennen.project_code = project.project_code
                WHERE
                    verwijderd IS NULL
                    AND NOT EXISTS (
                        SELECT eind_controle.einddatum
                        FROM eind_controle
                        WHERE eind_controle.project_code = project.project_code OR eind_controle.einddatum IS NOT NULL
                    )
            ");
        }

        public Project Get(int projectId)
        {
            return QueryFirstOrDefault($@"
                SELECT {this.GetSelects()}
                FROM project
                JOIN gebruiker ON project.laatst_aangepast_gebruiker = gebruiker.gebruiker_code
                JOIN locatie ON project.locatie_code = locatie.locatie_code
                LEFT OUTER JOIN verkennen ON verkennen.project_code = project.project_code
                WHERE
                    project.project_code = @projectId
                    AND verwijderd IS NULL
                    AND NOT EXISTS (
                        SELECT eind_controle.einddatum
                        FROM eind_controle
                        WHERE eind_controle.project_code = project.project_code OR eind_controle.einddatum IS NOT NULL
                    )
            ", new {projectId});
        }

        /// <summary>
        /// Get valid SQL UPDATE query of the properties
        /// </summary>
        /// <returns>SQL UPDATE Query</returns>
        protected override string GetUpdates()
        {
            Dictionary<string, string> data = this.DataMap
                .Where(kv => kv.Value != "Id")
                .Where(kv => kv.Key.StartsWith(this.TableName))
                .ToDictionary(i => i.Key, i => i.Value);

            return $@"
                UPDATE {this.TableName}
                SET {GetSelects(data, " = @")}
                WHERE project_code = @id
            ";
        }

        public Project Add(ProjectPost project)
        {
            return Execute(this.GetInserts(), project);
        }

        public Project Edit(ProjectPost project)
        {
            return Execute(this.GetUpdates(), project);
        }

        public Project Delete(int id)
        {
            return Execute($@"
                UPDATE {this.TableName}
                SET verwijderd = GETDATE()
                WHERE project_code = @id
            ", new { id });
        }
    }
}