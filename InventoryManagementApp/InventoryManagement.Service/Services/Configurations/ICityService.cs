using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;

namespace InventoryManagement.Service.Services.Configurations
{
    public interface ICityService:IBaseService<City>
    {
        Task<Dropdown<CityModel>> GetDropdownAsync(long? stateId = null, string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CityModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CityModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CityModel> AddCityDetailsAsync(CityModel model);
        Task<CityModel> UpdateCityDetailsAsync(long cityId, CityModel model);
        Task<CityModel> UpdateCityDetailsAsync(long cityId, string model);
        Task<CityModel> GetCityDetailsAsync(long cityId);
    }
}
