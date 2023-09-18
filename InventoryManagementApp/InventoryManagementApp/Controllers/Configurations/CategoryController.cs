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
    public class CategoryController : GenericBaseController<Category>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) : base(categoryService)
        {
            this._categoryService = categoryService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _categoryService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _categoryService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _categoryService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryDetailAsync(long categoryId)
        {
            var res = await _categoryService.GetCategoryDetailsAsync(categoryId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCategoryDetailAsync([FromForm] CategoryModel model)
        {
            var res = await _categoryService.AddCategoryDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryDetailAsync(long categoryId, [FromForm] CategoryModel model)
        {

            var res = await _categoryService.UpdateCategoryDetailsAsync(categoryId, model);

            return new ApiOkActionResult(res);
        }
    }
}
