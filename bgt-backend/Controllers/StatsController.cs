using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/stats")]
    public class StatsController : Controller
    {
        private readonly StatsRepository _repo = new StatsRepository();

        [HttpGet]
        public Response GetStats()
        {
            dynamic measurementTypes = this._repo.GetMeasurementTypes();
            dynamic projectsCount = this._repo.GetProjectsCount()[0];

            return new Response(this.Response, new
            {
                measurementTypes,
                projectsCount
            });
        }
    }
}