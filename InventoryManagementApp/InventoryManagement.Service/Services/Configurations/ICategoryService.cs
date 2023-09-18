using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement.Service.Services.Configurations
{
    public interface ICategoryService:IBaseService<Category>
    {
        Task<Dropdown<CategoryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CategoryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CategoryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CategoryModel> AddCategoryDetailsAsync(CategoryModel model);
        Task<CategoryModel> UpdateCategoryDetailsAsync(long categoryId, CategoryModel model);
        Task<CategoryModel> UpdateCategoryDetailsAsync(long categoryId, string model, List<IFormFile> images);
        Task<CategoryModel> GetCategoryDetailsAsync(long categoryId);
    }
}
