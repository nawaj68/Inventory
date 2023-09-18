using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using InventoryManagement.Service.Models.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public interface ICompanyService:IBaseService<Company>
    {
        Task<Dropdown<CompanyModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize);
        Task<Paging<CompanyModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null);
        Task<Paging<CompanyModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null);
        Task<CompanyModel> AddCompanyDetailsAsync(CompanyModel model);
        Task<CompanyModel> UpdateCompanyDetailsAsync(long companyId, CompanyModel model);
        Task<CompanyModel> UpdateCompanyDetailsAsync(long companyId, string model);
        Task<CompanyModel> GetCompanyDetailsAsync(long companyId);
    }
}
