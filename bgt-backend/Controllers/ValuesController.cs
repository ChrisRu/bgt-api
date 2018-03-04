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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("/test")]
        public string Test()
        {
            return "oh boy";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }
    }
}
