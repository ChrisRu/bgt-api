﻿using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class FoundationController : Controller<Foundation>
    {
        protected override Repository<Foundation> _repo { get; set; } = new FoundationRepository();
    }
}