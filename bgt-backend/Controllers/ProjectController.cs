using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Repositories;
using BGTBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _repo = new ProjectRepository();

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAll()
        {
            IEnumerable<Project> results = await this._repo.GetAll();
            return results.Select(Filter);
        }

        [HttpGet("{id}")]
        public async Task<Project> Get(int id)
        {
            var result = await this._repo.Get(new Dictionary<string, string>
            {
                {"id", id.ToString()}
            });

            return Filter(result);
        }

        private static Project Filter(Project project)
        {
            project.Id = null;
            return project;
        }
    }
}
