using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using InventoryManagement.Service.Models.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface ICurrencyService:IBaseService<Currency>
    {
        Task<Dropdown<CurrencyModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CurrencyModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CurrencyModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CurrencyModel> AddCurrencyDetailsAsync(CurrencyModel model);
        Task<CurrencyModel> UpdateCurrencyDetailsAsync(long currencyId, CurrencyModel model);
        Task<CurrencyModel> UpdateCurrencyDetailsAsync(long currencyId, string model);
        Task<CurrencyModel> GetCurrencyDetailsAsync(long currencyId);
    }
}
