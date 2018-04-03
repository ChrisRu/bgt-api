using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        private readonly LocationRepository _locationRepo = new LocationRepository();
        private readonly ProjectRepository _repo = new ProjectRepository();
        private readonly UserRepository _userRepo = new UserRepository();


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
                    new Error(HttpStatusCode.NotFound, $"Kan {this._repo.TableName} niet vinden: " + error.Message));
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<Response> GetAll()
        {
            try
            {
                return new Response(this.Response, this._repo.GetAll());
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.NotFound, "Kan niet alle projecten ophalen: " + error.Message));
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<Response> Edit([FromBody] ProjectPost project)
        {
            try
            {
                project.LastEditedUser =
                    this._userRepo.Get(AuthenticationController.GetCurrentUsername(this.HttpContext)).Id;
                project.LastEditedDate = DateTimeOffset.Now;

                if (project.Location.Latitude != null && project.Location.Longtitude != null)
                {
                    project.LocationCode = this.GetLocation(project.Location.Longtitude, project.Location.Latitude).Id;
                }

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
                project.LocationCode = this.GetLocation(project.Location.Longtitude, project.Location.Latitude).Id;

                this._repo.Add(project);
                return new {success = true};
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, "Kan geen nieuw project aanmaken: " + error.Message));
            }
        }

        private Location GetLocation(string longitude, string latitude)
        {
            Location location = this._locationRepo.Get(longitude, latitude);

            if (location == null)
            {
                this._locationRepo.Add(new LocationPost
                {
                    Longtitude = longitude,
                    Latitude = latitude
                });
                this._locationRepo.Get(longitude, latitude);
            }

            return location;
        }
    }
}