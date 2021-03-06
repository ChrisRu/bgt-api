﻿using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/endcheck")]
    public class EndCheckController : Controller<EndCheck>
    {
        protected override Repository<EndCheck> _repo { get; set; } = new EndCheckRepository();
    }
}