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
    public class CityController : GenericBaseController<City>
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) : base(cityService)
        {
            this._cityService = cityService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(long? stateId = null, string searchText = null)
        {
            var res = await _cityService.GetDropdownAsync(stateId, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _cityService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _cityService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetCityDetailsAsync(long cityId)
        {
            var res = await _cityService.GetCityDetailsAsync(cityId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCityDetailsAsync([FromForm] CityModel model)
        {
            var res = await _cityService.AddCityDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{cityId}")]
        public async Task<IActionResult> UpdateCityDetailsAsync(long cityId, [FromForm] CityModel model)
        {

            var res = await _cityService.UpdateCityDetailsAsync(cityId, model);

            return new ApiOkActionResult(res);
        }
    }
}
