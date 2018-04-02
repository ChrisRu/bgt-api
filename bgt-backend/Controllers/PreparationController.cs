using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/preparation")]
    public class PreparationController : Controller<Preparation>
    {
        protected override Repository<Preparation> _repo { get; set; } = new PreparationRepository();
    }
}