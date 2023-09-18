using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IOpenQuantityService:IBaseService<OpenQuantity>
    {
        Task<Dropdown<OpenQuantityModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<OpenQuantityModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<OpenQuantityModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<OpenQuantityModel> AddOpenQuantityDetailsAsync(OpenQuantityModel model);
        Task<OpenQuantityModel> GetOpenQuantityDetailsAsync(long openQuantityId);
        Task<OpenQuantityModel> UpdateOpenQuantityDetailsAsync(long openQuantityId, OpenQuantityModel model);
        Task<OpenQuantityModel> UpdateOpenQuantityDetailsAsync(long openQuantityId, string model);

    }
}
