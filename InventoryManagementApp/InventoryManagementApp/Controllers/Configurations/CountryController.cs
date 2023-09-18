using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : GenericBaseController<Country>
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService) : base(countryService)
        {
            this._countryService = countryService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _countryService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _countryService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _countryService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetCountryDetailsAsync(long countryId)
        {
            var res = await _countryService.GetCountryDetailsAsync(countryId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCountryDetailsAsync([FromForm] CountryModel model)
        {
            var res = await _countryService.AddCountryDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{countryId}")]
        public async Task<IActionResult> UpdateCountryDetailsAsync(long countryId, [FromForm] CountryModel model)
        {

            var res = await _countryService.UpdateCountryDetailsAsync(countryId, model);

            return new ApiOkActionResult(res);
        }
    }
}
