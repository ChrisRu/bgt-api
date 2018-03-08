using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

        [HttpGet("/test")]
        [Authorize]
        public object Test()
        {
            return new {
                message = "oh boy"
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value: " + id;
        }
    }
}
