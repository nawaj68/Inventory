using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services
{
    public interface IPurchasesMasterService : IBaseService<PurchasesMaster>
    {
        Task<Dropdown<PurchasesMasterModel>> GetDropdownAsync(string search = null, int size = CommonVariables.DropdownSize);
        Task<Paging<PurchasesMasterModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<PurchasesMasterModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null);

        Task<PurchasesMasterModel> GetPurchasesMasterDetailAsync(long purchasesmasterId);
        Task<PurchasesMasterModel> AddPurchasesMasterDetailAsync(PurchasesMasterModel model);
        Task<PurchasesMasterModel> UpdatePurchasesMasterDetailAsync(long purchasesmasterId, PurchasesMasterModel model);
        Task<PurchasesMasterModel> UpdatePurchasesMasterDetailAsync(long purchasesmasterId, string model);
    }
}
