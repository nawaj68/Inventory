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

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IPurchaseDetailsService : IBaseService<PurchaseDetails>
    {
        Task<Dropdown<PurchaseDetailsModel>> GetDropdownAsync(string search = null, int size = CommonVariables.DropdownSize);
        Task<Paging<PurchaseDetailsModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<PurchaseDetailsModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null);

        Task<PurchaseDetailsModel> GetPurchaseDetailsDetailAsync(long purchaseDetailsId);
        Task<PurchaseDetailsModel> AddPurchaseDetailsDetailAsync(PurchaseDetailsModel model);
        Task<PurchaseDetailsModel> UpdatePurchaseDetailsDetailAsync(long purchaseDetailsId, PurchaseDetailsModel model);
        Task<PurchaseDetailsModel> UpdatePurchaseDetailsDetailAsync(long purchaseDetailsId, string model);
    }
}
