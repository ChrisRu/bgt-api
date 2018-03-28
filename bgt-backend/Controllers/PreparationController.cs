using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class PreparationController : Controller<Preparation>
    {
        protected override Repository<Preparation> _repo { get; set; } = new PreparationRepository();
    }
}