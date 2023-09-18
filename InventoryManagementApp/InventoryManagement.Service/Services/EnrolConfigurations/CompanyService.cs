using AutoMapper;
using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Service.Models.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<CompanyModel> AddCompanyDetailsAsync(CompanyModel model)
        {
            var entity = _mapper.Map<CompanyModel, Company>(model);
            await _unitOfWork.Repository<Company>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CompanyModel();
        }

        public async Task<CompanyModel> GetCompanyDetailsAsync(long companyId)
        {
            var data = await _unitOfWork.Repository<Company>().FirstOrDefaultAsync(f => f.Id == companyId,
               o => o.OrderBy(ob => ob.Id),
               i => i.User,
               i => i.Country,
               i => i.State,
               i => i.City);
            return _mapper.Map<Company, CompanyModel>(data);
        }

        public async Task<Dropdown<CompanyModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Company>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.CompanyName.Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new CompanyModel { Id = se.Id, CompanyName = se.CompanyName },
                 size);
            return data;
        }

        public async Task<Paging<CompanyModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Company>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.CompanyName.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
               i => i.Country,
               i => i.State,
               i => i.City);
            return data.ToPagingModel<Company, CompanyModel>(_mapper);
        }

        public async Task<Paging<CompanyModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Company>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.CompanyName.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se,
               i => i.User,
               i => i.Country,
               i => i.State,
               i => i.City);
            return data.ToPagingModel<Company, CompanyModel>(_mapper);
        }

        public async Task<CompanyModel> UpdateCompanyDetailsAsync(long companyId, CompanyModel model)
        {
            var entity = _mapper.Map<CompanyModel, Company>(model);
            await _unitOfWork.Repository<Company>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CompanyModel();
        }

        public async Task<CompanyModel> UpdateCompanyDetailsAsync(long companyId, string model)
        {
            var company = JsonConvert.DeserializeObject<CompanyModel>(model);

            var entity = _mapper.Map<CompanyModel, Company>(company);
            await _unitOfWork.Repository<Company>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CompanyModel();
        }

    }
}
