using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : GenericBaseController<Currency>
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService) : base(currencyService)
        {
            this._currencyService = currencyService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _currencyService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _currencyService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _currencyService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{currencyId}")]
        public async Task<IActionResult> GetCurrencyDetailsAsync(long currencyId)
        {
            var res = await _currencyService.GetCurrencyDetailsAsync(currencyId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCurrencyDetailsAsync([FromForm] CurrencyModel model)
        {
            var res = await _currencyService.AddCurrencyDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{currencyId}")]
        public async Task<IActionResult> UpdateCurrencyDetailsAsync(long currencyId, [FromForm] CurrencyModel model)
        {

            var res = await _currencyService.UpdateCurrencyDetailsAsync(currencyId, model);

            return new ApiOkActionResult(res);
        }
    }
}
