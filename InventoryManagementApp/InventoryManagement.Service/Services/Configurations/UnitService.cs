using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UnitService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<UnitModel> AddUnitDetailsAsync(UnitModel model)
        {
            var entity = _mapper.Map<UnitModel, Unit>(model);
            await _unitOfWork.Repository<Unit>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new UnitModel();
        }

        public async Task<Dropdown<UnitModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.pageSize)
        {
            var data = await _unitOfWork.Repository<Unit>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.UnitName.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new UnitModel { Id = se.Id, UnitName = se.UnitName },
                size);
            return data;
        }

        public async Task<Paging<UnitModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Unit>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.UnitName.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Unit, UnitModel>(_mapper);
        }

        public async Task<Paging<UnitModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Unit>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.UnitName.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Unit, UnitModel>(_mapper);

        }

        public async Task<UnitModel> GetUnitDetailsAsync(long unitId)
        {
            var data = await _unitOfWork.Repository<Unit>().FirstOrDefaultAsync(f => f.Id == unitId);
            return _mapper.Map<Unit, UnitModel>(data);
        }

        public async Task<UnitModel> UpdateUnitDetailsAsync(long unitId, UnitModel model)
        {
            var entity = _mapper.Map<UnitModel, Unit>(model);

            await _unitOfWork.Repository<Unit>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new UnitModel();
        }
    }
}
