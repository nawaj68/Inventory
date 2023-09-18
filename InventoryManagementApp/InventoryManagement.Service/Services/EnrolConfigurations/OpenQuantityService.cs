using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using MassTransit.Testing;
using Newtonsoft.Json;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class OpenQuantityService : BaseService<OpenQuantity>,IOpenQuantityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OpenQuantityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<OpenQuantityModel> AddOpenQuantityDetailsAsync(OpenQuantityModel model)
        {
            var entity = _mapper.Map<OpenQuantityModel, OpenQuantity>(model);
            await _unitOfWork.Repository<OpenQuantity>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new OpenQuantityModel();
        }

        public Task<Dropdown<OpenQuantityModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Paging<OpenQuantityModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<OpenQuantity>().GetPageAsync(pageIndex, pageSize,
                p=>(string.IsNullOrEmpty(filterText)|p.Item.ItemName.Contains(filterText)),
                o=>o.OrderBy(o=>o.Id),
                se=>se);
            return data.ToPagingModel<OpenQuantity, OpenQuantityModel>(_mapper);
        }

        public async Task<OpenQuantityModel> GetOpenQuantityDetailsAsync(long openQuantityId)
        {
            var data = await _unitOfWork.Repository<OpenQuantity>().FirstOrDefaultAsync(f=>f.Id== openQuantityId);
            return _mapper.Map<OpenQuantity, OpenQuantityModel>(data);
        }

        public async Task<Paging<OpenQuantityModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<OpenQuantity>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(searchText) | p.Item.ItemName.Contains(searchText)),
                o => o.OrderBy(o => o.Id),
                se => se);
            return data.ToPagingModel<OpenQuantity, OpenQuantityModel>(_mapper);
        }

        public async Task<OpenQuantityModel> UpdateOpenQuantityDetailsAsync(long openQuantityId, OpenQuantityModel model)
        {
            var entity = _mapper.Map<OpenQuantityModel, OpenQuantity>(model);
            await _unitOfWork.Repository<OpenQuantity>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new OpenQuantityModel();
        }

        public async Task<OpenQuantityModel> UpdateOpenQuantityDetailsAsync(long openQuantityId, string model)
        {
            var openQuantity = JsonConvert.DeserializeObject<OpenQuantityModel>(model);
            var entity = _mapper.Map<OpenQuantityModel, OpenQuantity>(openQuantity);
            await _unitOfWork.Repository<OpenQuantity>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new OpenQuantityModel();
        }
    }
}
