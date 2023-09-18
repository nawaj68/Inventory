using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IWareHouseRepository:IBaseService<WareHouse>
    {
        Task<Dropdown<WareHouseModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<WareHouseModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<WareHouseModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<WareHouseModel> AddWareHouseDetailsAsync(WareHouseModel model);
        Task<WareHouseModel> UpdateWareHouseDetailsAsync(long warehouseId, WareHouseModel model);
        Task<WareHouseModel> UpdateWareHouseDetailsAsync(long warehouseId, string model);
        Task<WareHouseModel> GetWareHouseDetailsAsync(long warehouseId);
    }
}
