//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace InventoryManagement.Core.Results
//{
//    public class ForbiddenResult : IHttpActionResult
//    {
//        private readonly HttpRequestMessage _request;
//        private readonly string _reason;

//        public ForbiddenResult(HttpRequestMessage request, string reason)
//        {
//            _request = request;
//            _reason = reason;
//        }

//        public ForbiddenResult(HttpRequestMessage request)
//        {
//            _request = request;
//            _reason = "Forbidden";
//        }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            var response = _request.CreateResponse(HttpStatusCode.Forbidden, _reason);
//            return Task.FromResult(response);
//        }
//    }
//    // usage : if(User.IsInRole("Readonly")) { return Forbidden(); }

//    public class NoContentResult : IHttpActionResult
//    {
//        private readonly HttpRequestMessage _request;
//        private readonly string _reason;

//        public NoContentResult(HttpRequestMessage request, string reason)
//        {
//            _request = request;
//            _reason = reason;
//        }

//        public NoContentResult(HttpRequestMessage request)
//        {
//            _request = request;
//            _reason = "No Content";
//        }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            var response = _request.CreateResponse(HttpStatusCode.NoContent, _reason);
//            return Task.FromResult(response);
//        }
//    }

//    public class JsonTextActionResult : IHttpActionResult
//    {
//        public HttpRequestMessage Request { get; }

//        public string JsonText { get; }

//        public JsonTextActionResult(HttpRequestMessage request, string jsonText)
//        {
//            Request = request;
//            JsonText = jsonText;
//        }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            return Task.FromResult(Execute());
//        }

//        public HttpResponseMessage Execute()
//        {
//            var response = this.Request.CreateResponse(HttpStatusCode.OK);
//            response.Content = new StringContent(JsonText, Encoding.UTF8, "application/json");

//            return response;
//        }
//    }

//    //usage :
//    //baseconroller.cs
//    //protected internal virtual JsonTextActionResult JsonText(string jsonText)
//    //{
//    //    return new JsonTextActionResult(Request, jsonText);
//    //}

//    //contrroler.cs
//    //return JsonText(json);
//}
