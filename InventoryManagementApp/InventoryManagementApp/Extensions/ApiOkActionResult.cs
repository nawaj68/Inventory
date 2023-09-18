using InventoryManagement.Core.Collections;
using InventoryManagement.Core.Extension;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Extensions
{
    public class ApiOkActionResult : IActionResult
    {
        private readonly object _result;

        public ApiOkActionResult(object result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (_result?.GetType().Name == typeof(Paging<>).Name)
            {
                var total = _result.GetPropValue("Total");
                var response = context.HttpContext.Response;
                response.Headers.Add("X-Total-Count", total.ToString());
            }

            var objectResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status200OK,
                Value = new ApiResponse(_result)
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }

}
