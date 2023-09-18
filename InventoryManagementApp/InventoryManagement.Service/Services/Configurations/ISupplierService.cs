using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public interface ISupplierService : IBaseService<Supplier>
    {
        Task<Dropdown<SupplierModel>> GetDropdownAsync(string search = null, int size = CommonVariables.DropdownSize);
        Task<Paging<SupplierModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<SupplierModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null);

        Task<SupplierModel> GetSupplierDetailAsync(long supplierId);
        Task<SupplierModel> AddSupplierDetailAsync(SupplierModel model);
        Task<SupplierModel> UpdateSupplierDetailAsync(long supplierId, SupplierModel model);
        Task<SupplierModel> UpdateSupplierDetailAsync(long supplierId, string model);
    }
}
