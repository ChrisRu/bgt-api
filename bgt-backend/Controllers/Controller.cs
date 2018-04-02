using System;
using System.Net;
using System.Threading.Tasks;
using BGTBackend.Models;
using BGTBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGTBackend.Controllers
{
    public abstract class Controller<T> : Controller
    {
        protected abstract Repository<T> _repo { get; set; }

        /// <summary>
        /// Get all items of this category
        /// </summary>
        /// <returns>Response with all the items</returns>
        [HttpGet]
        [Authorize]
        public async Task<Response> GetAll()
        {
            try
            {
                return new Response(this.Response, this._repo.GetAll());
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.NotFound, $"Kan niet alle {this._repo.TableName} ophalen: " + error.Message));
            }
        }

        /// <summary>
        /// Get an item by ID
        /// </summary>
        /// <param name="id">ID of item to fetch</param>
        /// <returns>Response with the item itsself</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Response> Get(int id)
        {
            try
            {
                return new Response(this.Response, this._repo.Get(id));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.NotFound, $"Kan {this._repo.TableName} niet vinden: " + error.Message));
            }
        }

        /// <summary>
        /// Edit an exisiting item
        /// </summary>
        /// <param name="value">Data of item to edit</param>
        /// <returns>Response that shows whether request has been successful</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response> Put([FromBody] T value)
        {
            try
            {
                return new Response(this.Response, this._repo.Edit(value));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, $"Kan {this._repo.TableName} niet aanpassen: " + error.Message));
            }
        }

        /// <summary>
        /// Edit an exisiting item
        /// </summary>
        /// <param name="value">Data of item to edit</param>
        /// <returns>Response that shows whether request has been successful</returns>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<Response> Patch([FromBody] T value)
        {
            try
            {
                return new Response(this.Response, this._repo.Edit(value));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, $"Kan {this._repo.TableName} niet aanpassen: " + error.Message));
            }
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <param name="value">Data of item to create</param>
        /// <returns>Response that shows whether request has been successful</returns>
        [HttpPost]
        [Authorize]
        public async Task<Response> Create([FromBody] T value)
        {
            try
            {
                return new Response(this.Response, this._repo.Add(value));
            }
            catch (Exception error)
            {
                return new Response(this.Response,
                    new Error(HttpStatusCode.BadRequest, $"Kan geen nieuwe {this._repo.TableName} aanmaken: " + error.Message));
            }
        }
    }
}