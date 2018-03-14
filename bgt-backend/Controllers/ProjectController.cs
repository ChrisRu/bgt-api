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
        public async Task<Project> Edit([FromBody] ProjectPost project)
        {
            try
            {
                project.LastEditedUser = this._userRepo.Get("test").Id;
                project.LastEditedDate = DateTimeOffset.Now;

                LocationPost locationPost = await Geocode(project.Location);
                Location location = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);

                if (location == null)
                {
                    this._locationRepo.Add(locationPost);
                    location = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);
                }

                project.LocationCode = location.Id;

                return this._repo.Add(project);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                await this.Error(Response.HttpContext, 405, error.Message, "Kan project niet aanpassen");
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<Project> Create([FromBody] ProjectPost project)
        {
            try
            {
                project.LastEditedUser = this._userRepo.Get("test").Id;
                project.LastEditedDate = DateTimeOffset.Now;

                LocationPost locationPost = await Geocode(project.Location);
                Location location = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);

                if (location == null)
                {
                    this._locationRepo.Add(locationPost);
                    location = this._locationRepo.Get(locationPost.Longtitude, locationPost.Latitude);
                }

                project.LocationCode = location.Id;

                return this._repo.Add(project);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                await this.Error(Response.HttpContext, 405, error.Message, "Kan geen nieuw project aanmaken");
                return null;
            }
        }

        private static async Task<LocationPost> Geocode(string location)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BGT API for geocoding");

                string json = await httpClient.GetStringAsync($"https://nominatim.openstreetmap.org/search/{location}?format=json&limit=1");
                dynamic data = JsonConvert.DeserializeObject(json);

                return new LocationPost { Latitude = data[0].lat, Longtitude = data[0].lon };
            }
        }
    }
}
