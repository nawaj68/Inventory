using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : GenericBaseController<Unit>
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService):base(unitService)
        {
            _unitService = unitService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _unitService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _unitService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _unitService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{unitId}")]
        public async Task<IActionResult> GetUnitDetailsAsync(long unitId)
        {
            var res = await _unitService.GetUnitDetailsAsync(unitId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddUnitDetailsAsync([FromForm] UnitModel model)
        {
            var res = await _unitService.AddUnitDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{unitId}")]
        public async Task<IActionResult> UpdateUnitDetailsAsync(long unitId, [FromForm] UnitModel unit)
        {

            var res = await _unitService.UpdateUnitDetailsAsync(unitId, unit);

            return new ApiOkActionResult(res);
        }
    }
}
