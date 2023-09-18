using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CountryModel> AddCountryDetailsAsync(CountryModel model)
        {
            var entity = _mapper.Map<CountryModel, Country>(model);
            await _unitOfWork.Repository<Country>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CountryModel();
        }

        public async Task<CountryModel> GetCountryDetailsAsync(long countryId)
        {
            var data = await _unitOfWork.Repository<Country>().FirstOrDefaultAsync(f => f.Id == countryId);
            return _mapper.Map<Country, CountryModel>(data);
        }

        public async Task<Dropdown<CountryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Country>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new CountryModel { Id = se.Id, Name = se.Name },
                 size);
            return data;
        }

        public async Task<Paging<CountryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Country>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.Name.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Country, CountryModel>(_mapper);
        }

        public async Task<Paging<CountryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Country>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Country, CountryModel>(_mapper);
        }

        public async Task<CountryModel> UpdateCountryDetailsAsync(long countryId, CountryModel model)
        {
            var entity = _mapper.Map<CountryModel, Country>(model);
            await _unitOfWork.Repository<Country>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CountryModel();
        }

        public async Task<CountryModel> UpdateCountryDetailsAsync(long countryId, string model)
        {
            var city = JsonConvert.DeserializeObject<CountryModel>(model);

            var entity = _mapper.Map<CountryModel, Country>(city);
            await _unitOfWork.Repository<Country>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CountryModel();
        }
    }
}
