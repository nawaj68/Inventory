using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
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
    public interface ICountryService:IBaseService<Country>
    {
        Task<Dropdown<CountryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CountryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CountryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CountryModel> AddCountryDetailsAsync(CountryModel model);
        Task<CountryModel> UpdateCountryDetailsAsync(long countryId, CountryModel model);
        Task<CountryModel> UpdateCountryDetailsAsync(long countryId, string model);
        Task<CountryModel> GetCountryDetailsAsync(long countryId);
    }
}
