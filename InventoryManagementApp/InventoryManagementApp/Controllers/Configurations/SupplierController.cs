using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : GenericBaseController<Supplier>
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService) : base(supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _supplierService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _supplierService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _supplierService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierDetailAsync(long supplierId)
        {
            var res = await _supplierService.GetSupplierDetailAsync(supplierId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddSupplierDetailAsync([FromForm] SupplierModel model)
        {
            var res = await _supplierService.AddSupplierDetailAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplierDetailAsync(long supplierId, [FromForm] SupplierModel supplier)
        {

            var res = await _supplierService.UpdateSupplierDetailAsync(supplierId, supplier);

            return new ApiOkActionResult(res);
        }
    }
}
