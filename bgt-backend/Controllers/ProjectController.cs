using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Repositories;
using BGTBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _repo = new ProjectRepository();

        [HttpGet]
        public Task<IEnumerable<Project>> GetAll()
        {
            return this._repo.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Project> Get(int id)
        {
            return this._repo.Get(id);
        }

        [HttpPost]
        public async Task<Project> Create([FromBody] Project project)
        {
            int result = await this._repo.Add(project);

            return await this._repo.Get(result);
        }
    }
}
