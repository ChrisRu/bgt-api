using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("[controller]")]
    public class PreparationController : Controller
    {
        private readonly PreparationRepository _repo = new PreparationRepository();

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Response> Get(int id)
        {
            try
            {
                return new Response(this.Response, this._repo.Get(id));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.NotFound, "Kan project niet vinden: " + error.Message));
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<Response> Edit(int id, [FromBody] ProjectPost project)
        {
            try
            {
                Project previousProject = this._repo.Get(id);

                project.LastEditedUser =
                    this._userRepo.Get(AuthenticationController.GetCurrentUsername(this.HttpContext)).Id;
                project.LastEditedDate = DateTimeOffset.Now;
                project.LocationCode = !string.IsNullOrEmpty(project.Location)
                    ? (await this.GetLocation(project.Location)).Id
                    : previousProject.Location.Id;

                this._repo.Edit(project);
                return new Response(this.Response, project);
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, "Kan project niet wijzigen: " + error.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Create([FromBody] ProjectPost project)
        {
            try
            {
                project.LastEditedUser =
                    this._userRepo.Get(AuthenticationController.GetCurrentUsername(this.HttpContext)).Id;
                project.LastEditedDate = DateTimeOffset.Now;
                project.LocationCode = (await this.GetLocation(project.Location)).Id;

                this._repo.Add(project);
                return new {success = true};
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, "Kan geen nieuw project aanmaken: " + error.Message));
            }
        }
    }
}