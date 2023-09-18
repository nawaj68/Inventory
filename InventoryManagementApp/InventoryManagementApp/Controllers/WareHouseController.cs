using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WareHouseController : GenericBaseController<WareHouse>
    {
        private readonly IWareHouseRepository _wareHouse;
        public WareHouseController(IWareHouseRepository wareHouse) : base(wareHouse)
        {
            this._wareHouse = wareHouse;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _wareHouse.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _wareHouse.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _wareHouse.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{warehouseId}")]
        public async Task<IActionResult> GetWareHouseDetailsAsync(long warehouseId)
        {
            var res = await _wareHouse.GetWareHouseDetailsAsync(warehouseId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddWareHouseDetailsAsync([FromForm] WareHouseModel model)
        {
            var res = await _wareHouse.AddWareHouseDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{warehouseId}")]
        public async Task<IActionResult> UpdateWareHouseDetailsAsync(long warehouseId, [FromForm] WareHouseModel model)
        {

            var res = await _wareHouse.UpdateWareHouseDetailsAsync(warehouseId, model);

            return new ApiOkActionResult(res);
        }
    }
}
