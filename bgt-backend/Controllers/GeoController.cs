using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    [Route("api/geocoding")]
    public class GeoController : Controller
    {
        private readonly MemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        private const string SearchURL = "https://nominatim.openstreetmap.org/search?format=jsonv2&addressdetails=1&accept-language=nl";
        private const string ReverseSearchURL = "https://nominatim.openstreetmap.org/reverse?format=jsonv2&addressdetails=1&accept-language=nl";

        [HttpGet]
        [Route("search")]
        [Authorize]
        public Task<object> Search([FromQuery] string location)
        {
            string url = $"{SearchURL}&q={location}";
            return Request(url);
        }

        [HttpGet]
        [Route("reverse")]
        [Authorize]
        public Task<object> Reverse([FromQuery] string lat, string lon)
        {
            string url = $"{ReverseSearchURL}&lat={lat}&lon={lon}";
            return Request(url);
        }

        private async Task<object> Request(string url)
        {
            var res = this._memoryCache.Get(url);
            if (res != null)
            {
                return res;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.UserAgent, "BGT API");
            request.Headers.Add(HttpRequestHeader.Referer, "denhaag.azurewebsites.net");

            using(HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                var result = JsonConvert.DeserializeObject(await reader.ReadToEndAsync());
                this._memoryCache.Set(url, result, TimeSpan.FromHours(1));
                return result;
            }
        }
    }
}