using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        private readonly LocationRepository _locationRepo = new LocationRepository();
        private readonly ProjectRepository _repo = new ProjectRepository();
        private readonly UserRepository _userRepo = new UserRepository();

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

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response> Edit(int id, [FromBody] ProjectPost project)
        {
            try
            {
                if (id != project.Id) throw new Exception("Id van het project is anders dan de endpoint");

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
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BGT API for geocoding");

                string json =
                    await httpClient.GetStringAsync(
                        $"https://nominatim.openstreetmap.org/search/{location}?format=json&limit=1");
                dynamic data = JsonConvert.DeserializeObject(json);

                if (data == null) throw new Exception("Locatie niet gevonden");

                return new LocationPost {Latitude = data[0].lat, Longtitude = data[0].lon};
            }
        }
    }
}