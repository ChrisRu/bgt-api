using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BGTBackend.Repositories;
using BGTBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    [Route("api/projects")]
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _repo = new ProjectRepository();
        private readonly LocationRepository _locationRepo = new LocationRepository();
        private readonly UserRepository _userRepo = new UserRepository();

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
                await Error(Response.HttpContext, 404, error.Message, "Kan niet alle projecten ophalen");
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
                await Error(Response.HttpContext, 404, error.Message, "Project niet gevonden");
                return null;
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<object> Edit(int id, [FromBody] ProjectPost project)
        {
            try
            {
                if (id != project.Id)
                {
                    throw new Exception("Id van het project is anders dan de endpoint");
                }

                project.LastEditedUser = this._userRepo.Get(AuthenticationController.GetCurrentUsername(this.HttpContext)).Id;
                project.LastEditedDate = DateTimeOffset.Now;
                project.LocationCode = (await GetLocation(project.Location)).Id;

                this._repo.Edit(project);
                return new {success = true};
            }
            catch (Exception error)
            {
                await Error(Response.HttpContext, 405, error.Message, "Kan project niet wijzigen: " + error.Message);
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Create([FromBody] ProjectPost project)
        {
            try
            {
                project.LastEditedUser = this._userRepo.Get(AuthenticationController.GetCurrentUsername(this.HttpContext)).Id;
                project.LastEditedDate = DateTimeOffset.Now;
                project.LocationCode = (await GetLocation(project.Location)).Id;

                this._repo.Add(project);
                return new {success = true};
            }
            catch (Exception error)
            {
                await Error(Response.HttpContext, 405, error.Message, "Kan geen nieuw project aanmaken: " + error.Message);
                return null;
            }
        }

        private async Task<Location> GetLocation(string location)
        {
            LocationPost locationPost = await Geocode(location);
            Location dbLocation = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);

            if (dbLocation == null)
            {
                this._locationRepo.Add(locationPost);
                dbLocation = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);
            }

            return dbLocation;
        }

        private static async Task<LocationPost> Geocode(string location)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BGT API for geocoding");

                string json = await httpClient.GetStringAsync($"https://nominatim.openstreetmap.org/search/{location}?format=json&limit=1");
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data == null)
                {
                    throw new Exception("Locatie niet gevonden");
                }

                return new LocationPost { Latitude = data[0].lat, Longtitude = data[0].lon };
            }
        }
    }
}
