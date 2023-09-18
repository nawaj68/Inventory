using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenQuentityController : GenericBaseController<OpenQuantity>
    {
        private readonly IOpenQuantityService _openQuantityService;

        public OpenQuentityController(IOpenQuantityService openQuantityService) : base(openQuantityService)
        {
            this._openQuantityService = openQuantityService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _openQuantityService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _openQuantityService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _openQuantityService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{openQuantityId}")]
        public async Task<IActionResult> GetOpenQuantityDetailsAsync(long openQuantityId)
        {
            var res = await _openQuantityService.GetOpenQuantityDetailsAsync(openQuantityId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddOpenQuantityDetailsAsync([FromForm] OpenQuantityModel model)
        {
            var res = await _openQuantityService.AddOpenQuantityDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{openQuantityId}")]
        public async Task<IActionResult> UpdateOpenQuantityDetailsAsync(long openQuantityId, [FromForm] OpenQuantityModel model)
        {

            var res = await _openQuantityService.UpdateOpenQuantityDetailsAsync(openQuantityId, model);

            return new ApiOkActionResult(res);
        }

    }
}
