using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface IDamageService:IBaseService<Damage>
    {
        Task<Dropdown<DamageModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<DamageModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<DamageModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<DamageModel> AddDamageDetailsAsync(DamageModel model);
        Task<DamageModel> UpdateDamageDetailsAsync(long damageId, DamageModel model);
        //Task<DamageModel> UpdateDamageDetailsAsync(long damageId, string model);
        Task<DamageModel> GetDamageDetailsAsync(long damageId);
    }
}
