using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using BGTBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BGTBackend.Controllers
{
    [Route("api/geocoding")]
    public class GeocodingController : Controller
    {
        private const string SearchURL =
            "https://bagviewer.kadaster.nl/lvbag/bag-viewer/api/suggest?count=5&offset=0&searchQuery={q}";

        private const string SearchDetailsURL =
            "https://bagviewer.kadaster.nl/lvbag/bag-viewer/api/searchByLsId?lsId={q}";

        private const string KadasterURL =
            "https://geodata.nationaalgeoregister.nl/bgtterugmeldingen/wfs?request=GetFeature&service=WFS&version=2.0.0&typeName=bgtterugmeldingen&outputFormat=application%2Fjson&bbox=69294,439441,87991,463824";

        private const string ReverseSearchURL =
            "https://nominatim.openstreetmap.org/reverse?format=jsonv2&addressdetails=1&accept-language=nl&email=16034198@student.hhs.nl";

        /// <summary>
        /// Geocode, get a location string
        /// </summary>
        /// <param name="location">Location to get data from</param>
        /// <returns>A response with the data</returns>
        [HttpGet]
        [Route("search/{location}")]
        [Authorize]
        public async Task<Response> Search(string location)
        {
            string url = $"{SearchURL.Replace("{q}", HttpUtility.UrlEncode(location.Replace("+", " ").ToLowerInvariant()))}";
            try
            {
                var result = await Request(url);
                return new Response(this.Response, result.lsIdDisplayStringPairs);
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadGateway, "Kan geen informatie ophalen: " + error.Message));
            }
        }

        /// <summary>
        /// Geocode, get a location string
        /// </summary>
        /// <param name="location">Location to get data from</param>
        /// <returns>A response with the data</returns>
        [HttpGet]
        [Route("getdetails/{id}")]
        [Authorize]
        public async Task<Response> GetDetails(string id)
        {
            string url = $"{SearchDetailsURL.Replace("{q}", HttpUtility.UrlEncode(id.ToLowerInvariant()))}";
            try
            {
                var result = await Request(url);
                return new Response(this.Response, result);
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
        [Route("reverse")]
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
        /// Get the BAG API Terugmeldingen
        /// </summary>
        /// <returns>List of unfinished tasks</returns>
        [HttpGet]
        [Route("terugmeldingen")]
        [Authorize]
        public async Task<Response> TerugMeldingen([FromQuery] bool getAll = false)
        {
            try
            {
                var fullResponse = await Request(KadasterURL);
                var filteredResponse = ((JArray) fullResponse.features).ToObject<List<dynamic>>().FindAll(feature => getAll || feature?.properties?.status == "Nieuw");
                return new Response(this.Response, filteredResponse);
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
        private static async Task<dynamic> Request(string url)
        {
            object res = Startup.MemoryCache.Get(url);
            if (res != null)
            {
                return res;
            }

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.UserAgent, "BGT API");
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