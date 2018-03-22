using System.Net;
using Microsoft.AspNetCore.Http;

namespace BGTBackend.Models
{
    public class Error
    {
        public Error(HttpStatusCode code, string message)
        {
            this.code = (int) code;
            this.message = message;
        }

        public Error(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public int code { get; }

        public string message { get; }
    }

    public class Response
    {
        public Response(HttpResponse response, Error error)
        {
            response.StatusCode = error.code;
            this.error = error;
            this.status = "error";
        }

        public Response(HttpResponse response, object data)
        {
            response.StatusCode = (int) HttpStatusCode.OK;
            this.data = data;
            this.status = "success";
        }

        public string status { get; }

        public Error error { get; }

        public object data { get; }
    }
}