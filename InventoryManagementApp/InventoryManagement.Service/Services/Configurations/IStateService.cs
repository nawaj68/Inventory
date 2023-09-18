using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
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
    public interface IStateService:IBaseService<State>
    {
        Task<Dropdown<StateModel>> GetDropdownAsync(long? countryId = null, string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<StateModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<StateModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<StateModel> AddStateDetailsAsync(StateModel model);
        Task<StateModel> UpdateStateDetailsAsync(long stateId, StateModel model);
        Task<StateModel> UpdateStateDetailsAsync(long stateId, string model);
        Task<StateModel> GetStateDetailsAsync(long stateId);
    }
}
