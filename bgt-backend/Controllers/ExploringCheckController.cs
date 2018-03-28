using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class ExporingCheckController : Controller<ExploringCheck>
    {
        protected override Repository<ExploringCheck> _repo { get; set; } = new ExploringCheckRepository();
    }
}