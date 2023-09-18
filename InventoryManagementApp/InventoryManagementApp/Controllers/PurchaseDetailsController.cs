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
    public class PurchaseDetailsController : GenericBaseController<PurchaseDetails>
    {
        private readonly IPurchaseDetailsService _purchaseDetailsService;
        public PurchaseDetailsController(IPurchaseDetailsService purchaseDetailsService) : base(purchaseDetailsService)
        {
            _purchaseDetailsService = purchaseDetailsService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _purchaseDetailsService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _purchaseDetailsService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _purchaseDetailsService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{purchaseDetailsId}")]
        public async Task<IActionResult> GetPurchaseDetailsDetailAsync(long purchaseDetailsId)
        {
            var res = await _purchaseDetailsService.GetPurchaseDetailsDetailAsync(purchaseDetailsId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddPurchaseDetailsDetailAsync([FromForm] PurchaseDetailsModel model)
        {
            var res = await _purchaseDetailsService.AddPurchaseDetailsDetailAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{purchaseDetailsId}")]
        public async Task<IActionResult> UpdatePurchaseDetailsDetailAsync(long purchaseDetailsId, [FromForm] PurchaseDetailsModel purchaseDetails)
        {

            var res = await _purchaseDetailsService.UpdatePurchaseDetailsDetailAsync(purchaseDetailsId, purchaseDetails);

            return new ApiOkActionResult(res);
        }
    }
}
