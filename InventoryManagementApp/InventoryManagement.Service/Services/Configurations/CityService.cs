using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using MassTransit.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CityModel> AddCityDetailsAsync(CityModel model)
        {
            var entity = _mapper.Map<CityModel, City>(model);
            await _unitOfWork.Repository<City>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CityModel();
        }

        public async Task<CityModel> GetCityDetailsAsync(long cityId)
        {
            var data = await _unitOfWork.Repository<City>().FirstOrDefaultAsync(f=>f.Id== cityId);
            return _mapper.Map<City, CityModel>(data);
        }

        public async Task<Dropdown<CityModel>> GetDropdownAsync(long? stateId = null, string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<City>().GetDropdownAsync(
                 p => ((string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                 && (stateId == null || p.StateId == stateId)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new CityModel { Id = se.Id, Name = se.Name, StateId = se.StateId },
                 size);
            return data;
        }

        public async Task<Paging<CityModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<City>().GetPageAsync( pageIndex,pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.Name.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<City, CityModel>(_mapper);
        }

        public async Task<Paging<CityModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<City>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty( searchText) | p.Name.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se,
                i=>i.State);
            return data.ToPagingModel<City, CityModel>(_mapper);
        }

        public async Task<CityModel> UpdateCityDetailsAsync(long cityId, CityModel model)
        {
            var entity = _mapper.Map<CityModel, City>(model);
            await _unitOfWork.Repository<City>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CityModel();
        }

        public async Task<CityModel> UpdateCityDetailsAsync(long cityId, string model)
        {
            var city = JsonConvert.DeserializeObject<CityModel>(model);

            var entity = _mapper.Map<CityModel, City>(city);
            await _unitOfWork.Repository<City>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CityModel();
        }
    }
}
