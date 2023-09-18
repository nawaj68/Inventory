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
    public class SubCategoryController : GenericBaseController<SubCategory>
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService) : base(subCategoryService)
        {
            this._subCategoryService = subCategoryService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _subCategoryService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _subCategoryService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _subCategoryService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{subCategoryId}")]
        public async Task<IActionResult> GetCategoryDetailAsync(long subCategoryId)
        {
            var res = await _subCategoryService.GetSubCategoryDetailsAsync(subCategoryId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCategoryDetailAsync([FromForm] SubCategoryModel model)
        {
            var res = await _subCategoryService.AddSubCategoryDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{subCategoryId}")]
        public async Task<IActionResult> UpdateCategoryDetailAsync(long subCategoryId, [FromForm] SubCategoryModel model)
        {

            var res = await _subCategoryService.UpdateSubCategoryDetailsAsync(subCategoryId, model);

            return new ApiOkActionResult(res);
        }
    }
}
