using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/exploringcheck")]
    public class ExporingCheckController : Controller<ExploringCheck>
    {
        protected override Repository<ExploringCheck> _repo { get; set; } = new ExploringCheckRepository();
    }
}