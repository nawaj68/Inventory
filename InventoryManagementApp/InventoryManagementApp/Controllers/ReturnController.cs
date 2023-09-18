using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : GenericBaseController<Return>
    {
        private readonly IReturnService _returnService;
        public ReturnController(IReturnService returnService) : base(returnService)
        {
            _returnService = returnService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _returnService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _returnService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _returnService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{returnId}")]
        public async Task<IActionResult> GetReturnDetailAsync(long returnId)
        {
            var res = await _returnService.GetReturnDetailAsync(returnId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddReturnDetailAsync([FromForm] ReturnModel model)
        {
            var res = await _returnService.AddReturnDetailAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{returnId}")]
        public async Task<IActionResult> UpdateReturnDetailAsync(long returnId, [FromForm] ReturnModel returns)
        {

            var res = await _returnService.UpdateReturnDetailAsync(returnId, returns);

            return new ApiOkActionResult(res);
        }
    }
}

