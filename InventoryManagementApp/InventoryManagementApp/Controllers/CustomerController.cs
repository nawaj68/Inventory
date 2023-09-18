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
    public class CustomerController : GenericBaseController<Customer>
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            this._customerService = customerService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _customerService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _customerService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _customerService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerDetailsAsync(long customerId)
        {
            var res = await _customerService.GetCustomerDetailsAsync(customerId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCustomerDetailsAsync([FromForm] CustomerModel model)
        {
            var res = await _customerService.AddCustomerDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomerDetailsAsync(long customerId, [FromForm] CustomerModel model)
        {

            var res = await _customerService.UpdateCustomerDetailsAsync(customerId, model);

            return new ApiOkActionResult(res);
        }

    }
}
