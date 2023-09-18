using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public interface ISubCategoryService:IBaseService<SubCategory>
    {
        Task<Dropdown<SubCategoryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<SubCategoryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<SubCategoryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<SubCategoryModel> AddSubCategoryDetailsAsync(SubCategoryModel model);
        Task<SubCategoryModel> UpdateSubCategoryDetailsAsync(long subCategoryId, SubCategoryModel model);
        Task<SubCategoryModel> UpdateSubCategoryDetailsAsync(long subCategoryId, string model, List<IFormFile> images);
        Task<SubCategoryModel> GetSubCategoryDetailsAsync(long subCategoryId);
    }
}
