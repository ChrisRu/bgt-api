using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementController : Controller<Measurement>
    {
        protected override Repository<Measurement> _repo { get; set; } = new MeasurementRepository();
    }
}