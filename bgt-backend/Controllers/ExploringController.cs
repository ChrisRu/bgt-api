using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/exploring")]
    public class ExploringController : Controller<Exploring>
    {
        protected override Repository<Exploring> _repo { get; set; } = new ExploringRepository();
    }
}