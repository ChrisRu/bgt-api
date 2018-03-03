using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Clients;
using BGTBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private ProjectRepository Repository { get; }

        public ProjectController()
        {
            this.Repository = new ProjectRepository();
        }
        
        // GET api/projects
        [HttpGet]
        public Task<IEnumerable<Project>> GetAll()
        {
            return this.Repository.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Project> Get(int id)
        {
            return this.Repository.Get(new Dictionary<string, string>
            {
                {"id", id.ToString()}
            });
        }
    }
}
