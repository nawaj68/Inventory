using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IItemService:IBaseService<Item>
    {
        Task<Dropdown<ItemModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<ItemModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<ItemModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<ItemModel> AddItemDetailsAsync(ItemModel model);
        Task<ItemModel> UpdateItemDetailsAsync(long itemId, ItemModel model);
        Task<ItemModel> UpdateItemDetailsAsync(long itemId, string model);
        Task<ItemModel> GetItemDetailsAsync(long itemId);
    }
}
