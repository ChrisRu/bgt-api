using System;
using System.Linq;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/stats")]
    public class StatsController : Controller
    {
        private readonly StatsRepository _repo = new StatsRepository();

        private readonly ProjectRepository _projectRepo = new ProjectRepository();

        [HttpGet]
        public Response GetStats()
        {
            dynamic measurementTypes = this._repo.GetMeasurementTypes();
            dynamic projectsCount = this._repo.GetProjectsCount()[0];
            dynamic priorities = this._projectRepo.GetAll()
                .Where(value => value.ExploreDate != null && value.ExploreDate != DateTimeOffset.MinValue)
                .OrderBy(value => value.ExploreDate);

            return new Response(this.Response, new
            {
                measurementTypes,
                projectsCount,
                priorities
            });
        }
    }
}