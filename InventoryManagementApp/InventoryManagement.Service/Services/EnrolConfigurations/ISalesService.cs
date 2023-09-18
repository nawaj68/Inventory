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
    public interface ISalesService : IBaseService<Sales>
    {
        Task<Dropdown<SalesModel>> GetDropdownAsync(string search = null, int size = CommonVariables.DropdownSize);
        Task<Paging<SalesModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<SalesModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null);

        Task<SalesModel> GetSalesDetailAsync(long salesId);
        Task<SalesModel> AddSalesDetailAsync(SalesModel model);
        Task<SalesModel> UpdateSalesDetailAsync(long salesId, SalesModel model);
        Task<SalesModel> UpdateSalesDetailAsync(long salesId, string model);
    }
}
