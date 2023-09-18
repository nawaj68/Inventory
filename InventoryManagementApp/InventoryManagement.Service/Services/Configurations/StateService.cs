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
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class StateService : BaseService<State>, IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StateService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<StateModel> AddStateDetailsAsync(StateModel model)
        {
            var entity = _mapper.Map<StateModel, State>(model);
            await _unitOfWork.Repository<State>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new StateModel();
        }

        public async Task<Dropdown<StateModel>> GetDropdownAsync(long? countryId = null,
            string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<State>().GetDropdownAsync(
                p => ((string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                        && (countryId == null || p.CountryId == countryId)),
                o => o.OrderBy(ob => ob.Id),
                se => new StateModel { Id = se.Id, Name = se.Name, CountryId = se.CountryId },
                size);

            return data;
        }

        public async Task<Paging<StateModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<State>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(filterText) | p.Name.Contains(filterText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<State, StateModel>(_mapper);
        }

        public async Task<Paging<StateModel>> GetSearchAsync(int pageIndex = 0, int pageSize = 10, string searchText = null)
        {
            var data = await _unitOfWork.Repository<State>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se,
                i=>i.Country);
            return data.ToPagingModel<State, StateModel>(_mapper);
        }

        public async Task<StateModel> GetStateDetailsAsync(long stateId)
        {
            var data = await _unitOfWork.Repository<State>().FirstOrDefaultAsync(f=>f.Id== stateId);
            return _mapper.Map<State, StateModel>(data);
        }

        public async Task<StateModel> UpdateStateDetailsAsync(long stateId, StateModel model)
        {
            var entity = _mapper.Map<StateModel, State>(model);
            await _unitOfWork.Repository<State>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new StateModel();
        }

        public async Task<StateModel> UpdateStateDetailsAsync(long stateId, string model)
        {
            var state = JsonConvert.DeserializeObject<StateModel>(model);

            var entity = _mapper.Map<StateModel, State>(state);
            await _unitOfWork.Repository<State>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new StateModel();


        }
    }
}
