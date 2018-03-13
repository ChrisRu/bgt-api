using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        protected Task Send(HttpContext context, object message)
        {
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(message));
        }

        protected Task Error(HttpContext context, int code, string error, string userMessage = "")
        {
            context.Response.StatusCode = code;
            return this.Send(context, new { error, userMessage });
        }
    }
}