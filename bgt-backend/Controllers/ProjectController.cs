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
                return this._repo.GetAll();
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
                return this._repo.Get(id);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 404, error.Message, "Project niet gevonden");
                return null;
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Project> Edit(int id, [FromBody] Project project)
        {
            try
            {
                if (id != project.Id)
                {
                    throw new Exception("Id does not match");
                }

                return this._repo.Add(project);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan project niet aanpassen");
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<Project> Create([FromBody] Project project)
        {
            try
            {
                return this._repo.Add(project);
            }
            catch (Exception error)
            {
                await this.Error(Response.HttpContext, 405, error.Message, "Kan geen nieuw project aanmaken");
                return null;
            }
        }
    }
}
