using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class WareHouseRepository : BaseService<WareHouse>, IWareHouseRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WareHouseRepository(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<WareHouseModel> AddWareHouseDetailsAsync(WareHouseModel model)
        {
            var entity = _mapper.Map<WareHouseModel, WareHouse>(model);
            await _unitOfWork.Repository<WareHouse>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new WareHouseModel();
        }

        public async Task<Dropdown<WareHouseModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.pageSize)
        {
            var data = await _unitOfWork.Repository<WareHouse>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new WareHouseModel { Id = se.Id, Name = se.Name },
                 size);
            return data;
        }

        public async Task<Paging<WareHouseModel>> GetFilterAsync(int pageIndex = 0, int pageSize = 10, string filterText = null)
        {
            var data = await _unitOfWork.Repository<WareHouse>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.Name.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<WareHouse, WareHouseModel>(_mapper);
        }

        public async Task<Paging<WareHouseModel>> GetSearchAsync(int pageIndex = 0, int pageSize = 10, string searchText = null)
        {
            var data = await _unitOfWork.Repository<WareHouse>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se,i=>i.Company, i=>i.Country);
            return data.ToPagingModel<WareHouse, WareHouseModel>(_mapper);
        }

        public async Task<WareHouseModel> GetWareHouseDetailsAsync(long warehouseId)
        {
            var data = await _unitOfWork.Repository<WareHouse>().FirstOrDefaultAsync(f => f.Id == warehouseId);
            return _mapper.Map<WareHouse, WareHouseModel>(data);
        }

        public async Task<WareHouseModel> UpdateWareHouseDetailsAsync(long warehouseId, WareHouseModel model)
        {
            var entity = _mapper.Map<WareHouseModel, WareHouse>(model);
            await _unitOfWork.Repository<WareHouse>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new WareHouseModel();
        }

        public async Task<WareHouseModel> UpdateWareHouseDetailsAsync(long warehouseId, string model)
        {
            var city = JsonConvert.DeserializeObject<WareHouseModel>(model);

            var entity = _mapper.Map<WareHouseModel, WareHouse>(city);
            await _unitOfWork.Repository<WareHouse>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new WareHouseModel();
        }
    }
}
