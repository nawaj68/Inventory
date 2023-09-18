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
    public class ItemController : GenericBaseController<Item>
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) : base(itemService)
        {
            this._itemService = itemService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _itemService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _itemService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _itemService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemDetailsAsync(long itemId)
        {
            var res = await _itemService.GetItemDetailsAsync(itemId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddItemDetailsAsync([FromForm] ItemModel model)
        {
            var res = await _itemService.AddItemDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItemDetailsAsync(long itemId, [FromForm] ItemModel model)
        {

            var res = await _itemService.UpdateItemDetailsAsync(itemId, model);

            return new ApiOkActionResult(res);
        }
    }
}
