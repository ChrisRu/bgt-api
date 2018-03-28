using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class ProcessingController : Controller<Processing>
    {
        protected override Repository<Processing> _repo { get; set; } = new ProcessingRepository();
    }
}