using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IReturnService: IBaseService<Return>
    {
        Task<Dropdown<ReturnModel>> GetDropdownAsync(string search = null, int size = CommonVariables.DropdownSize);
        Task<Paging<ReturnModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<ReturnModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null);

        Task<ReturnModel> GetReturnDetailAsync(long returnId);
        Task<ReturnModel> AddReturnDetailAsync(ReturnModel model);
        Task<ReturnModel> UpdateReturnDetailAsync(long returnId, ReturnModel model);
        Task<ReturnModel> UpdateReturnDetailAsync(long returnId, string model);
    }
}
