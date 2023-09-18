using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesMasterController : GenericBaseController<PurchasesMaster>
    {
        private readonly IPurchasesMasterService _purchasesmasterService;
        public PurchasesMasterController(IPurchasesMasterService purchasesmasterService) : base(purchasesmasterService)
        {
            _purchasesmasterService = purchasesmasterService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _purchasesmasterService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _purchasesmasterService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _purchasesmasterService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{purchasesmasterId}")]
        public async Task<IActionResult> GetPurchasesMasterDetailAsync(long purchasesmasterId)
        {
            var res = await _purchasesmasterService.GetPurchasesMasterDetailAsync(purchasesmasterId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddPurchasesMasterDetailAsync([FromForm] PurchasesMasterModel model)
        {
            var res = await _purchasesmasterService.AddPurchasesMasterDetailAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{purchasesmasterId}")]
        public async Task<IActionResult> UpdatePurchasesMasterDetailAsync(long purchasesmasterId, [FromForm] PurchasesMasterModel purchasesmaster)
        {

            var res = await _purchasesmasterService.UpdatePurchasesMasterDetailAsync(purchasesmasterId, purchasesmaster);

            return new ApiOkActionResult(res);
        }
    }
}
