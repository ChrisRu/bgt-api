using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using BGTBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BGTBackend.Controllers
{
    [Route("api/[controller]")]
    public class GeocodingController : Controller
    {
        private const string SearchURL =
            "https://nominatim.openstreetmap.org/search/nl/den%20haag/{q}?format=jsonv2&addressdetails=1&accept-language=nl";

        private const string ReverseSearchURL =
            "https://nominatim.openstreetmap.org/reverse?format=jsonv2&addressdetails=1&accept-language=nl&email=16034198@student.hhs.nl";

        /// <summary>
        /// Geocode, get coordinates by a location string
        /// </summary>
        /// <param name="location">Location to get data from</param>
        /// <returns>A response with the data</returns>
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<Response> Search([FromQuery] string location)
        {
            string url = $"{SearchURL.Replace("{q}", HttpUtility.UrlEncode(location.Replace("+", " ").ToLowerInvariant()))}";
            Console.WriteLine(url);
            try
            {
                return new Response(this.Response, await Request(url));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadGateway, "Kan geen informatie ophalen: " + error.Message));
            }
        }

        /// <summary>
        /// Reverse Geocode, get information about the coordinates
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>A response with the data</returns>
        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<Response> Reverse([FromQuery] string lat, string lon)
        {
            lat = HttpUtility.UrlEncode(lat.ToLowerInvariant());
            lon = HttpUtility.UrlEncode(lon.ToLowerInvariant());
            string url = $"{ReverseSearchURL}&lat={lat}&lon={lon}";
            try
            {
                return new Response(this.Response, await Request(url));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadGateway, "Kan geen informatie ophalen: " + error.Message));
            }
        }

        /// <summary>
        /// Do a web request to get the data from a url
        /// </summary>
        /// <param name="url">The URL to fetch from</param>
        /// <returns>Dynamic object with the result</returns>
        private static async Task<object> Request(string url)
        {
            object res = Startup.MemoryCache.Get(url);
            if (res != null) return res;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.UserAgent, "BGT API - 16034198@student.hhs.nl");
            request.Headers.Add(HttpRequestHeader.Referer, "denhaag.azurewebsites.net");

            using (HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                object result = JsonConvert.DeserializeObject(await reader.ReadToEndAsync());
                Startup.MemoryCache.Set(url, result, TimeSpan.FromHours(1));
                return result;
            }
        }
    }
}