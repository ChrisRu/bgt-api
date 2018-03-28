using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementMapController : Controller<MeasurementMap>
    {
        protected override Repository<MeasurementMap> _repo { get; set; } = new MeasurementMapRepository();
    }
}