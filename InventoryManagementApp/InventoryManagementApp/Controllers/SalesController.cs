using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : GenericBaseController<Sales>
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService) : base(salesService)
        {
            _salesService = salesService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _salesService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _salesService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _salesService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{salesId}")]
        public async Task<IActionResult> GetSalesDetailAsync(long salesId)
        {
            var res = await _salesService.GetSalesDetailAsync(salesId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddSalesDetailAsync([FromForm] SalesModel model)
        {
            var res = await _salesService.AddSalesDetailAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{salesId}")]
        public async Task<IActionResult> UpdateSalesDetailAsync(long salesId, [FromForm] SalesModel sales)
        {

            var res = await _salesService.UpdateSalesDetailAsync(salesId, sales);

            return new ApiOkActionResult(res);
        }
    }
}

