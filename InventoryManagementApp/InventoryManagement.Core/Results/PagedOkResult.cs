//using System.Collections;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Formatting;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Results;

//namespace InventoryManagement.Common.Results
//{
//    public class PagedOkResult<T> : OkNegotiatedContentResult<T>
//        where T : IEnumerable
//    {
//        public int TotalCount { get; }

//        public PagedOkResult(T content, int totalCount, ApiController controller)
//            : base(content, controller)
//        {
//            TotalCount = totalCount;
//        }

//        public PagedOkResult(T content, int totalCount, IContentNegotiator contentNegotiator, HttpRequestMessage request,
//            IEnumerable<MediaTypeFormatter> formatters)
//            : base(content, contentNegotiator, request, formatters)
//        {
//            TotalCount = totalCount;
//        }

//        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            var response = await base.ExecuteAsync(cancellationToken);
//            response.Headers.Add("X-Total-Count", $"{TotalCount}");
//            return response;
//        }
//    }
//}
