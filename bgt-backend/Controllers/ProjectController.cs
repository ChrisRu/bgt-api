using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BGTBackend.Repositories;
using BGTBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _repo = new ProjectRepository();

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Project>> GetAll()
        {
            try
            {
                return await this._repo.GetAll();
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 404, error.Message, "Kan niet alle projecten ophalen");
                return null;
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Project> Get(int id)
        {
            try
            {
                return await this._repo.Get(id);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 404, error.Message, "Project niet gevonden");
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<Project> Create([FromBody] Project project)
        {
            try
            {
                return await this._repo.Add(project);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan geen nieuw project aanmaken");
                return null;
            }
        }
    }
}
