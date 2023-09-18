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
    public interface ICustomerService:IBaseService<Customer>
    {
        Task<Dropdown<CustomerModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CustomerModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CustomerModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CustomerModel> AddCustomerDetailsAsync(CustomerModel model);
        Task<CustomerModel> UpdateCustomerDetailsAsync(long customerId, CustomerModel model);
        Task<CustomerModel> UpdateCustomerDetailsAsync(long customerId, string model);
        Task<CustomerModel> GetCustomerDetailsAsync(long customerId);
    }
}
