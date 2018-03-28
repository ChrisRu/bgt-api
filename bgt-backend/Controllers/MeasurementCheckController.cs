using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class MeaasurementCheckController : Controller<MeasurementCheck>
    {
        protected override Repository<MeasurementCheck> _repo { get; set; } = new MeasurementCheckRepository();
    }
}