using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    [Route("[controller]")]
    public class GeocodingController : Controller
    {
        private const string SearchURL =
            "https://nominatim.openstreetmap.org/search/nl/den haag/{q}?format=jsonv2&addressdetails=1&accept-language=nl";

        private const string ReverseSearchURL =
            "https://nominatim.openstreetmap.org/reverse?format=jsonv2&addressdetails=1&accept-language=nl";

        private readonly MemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<Response> Search([FromQuery] string location)
        {
            string url = $"{SearchURL.Replace("{q}", location)}";
            try
            {
                return new Response(this.Response, await this.Request(url));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadGateway, "Kan geen informatie ophalen: " + error.Message));
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<Response> Reverse([FromQuery] string lat, string lon)
        {
            string url = $"{ReverseSearchURL}&lat={lat}&lon={lon}";
            try
            {
                return new Response(this.Response, await this.Request(url));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadGateway, "Kan geen informatie ophalen: " + error.Message));
            }
        }

        private async Task<object> Request(string url)
        {
            object res = this._memoryCache.Get(url);
            if (res != null) return res;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.UserAgent, "BGT API");
            request.Headers.Add(HttpRequestHeader.Referer, "denhaag.azurewebsites.net");

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                object result = JsonConvert.DeserializeObject(await reader.ReadToEndAsync());
                this._memoryCache.Set(url, result, TimeSpan.FromHours(1));
                return result;
            }
        }
    }
}