using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/stereokarteren")]
    public class StereokarterenController : Controller<Stereokarteren>
    {
        protected override Repository<Stereokarteren> _repo { get; set; } = new StereokarterenRepository();
    }
}