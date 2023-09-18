using AutoMapper;
using InventoryManagement.Core.Collections;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using Newtonsoft.Json;
using InventoryManagement.Service.Models.Enrols;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    internal class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CurrencyModel> AddCurrencyDetailsAsync(CurrencyModel model)
        {
            var entity = _mapper.Map<CurrencyModel, Currency>(model);
            await _unitOfWork.Repository<Currency>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CurrencyModel();
        }

        public async Task<CurrencyModel> GetCurrencyDetailsAsync(long currencyId)
        {
            var data = await _unitOfWork.Repository<Currency>().FirstOrDefaultAsync(f => f.Id == currencyId);
            return _mapper.Map<Currency, CurrencyModel>(data);
        }

        public async Task<Dropdown<CurrencyModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Currency>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.CurrencyName.Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new CurrencyModel { Id = se.Id, CurrencyName = se.CurrencyName },
                 size);
            return data;
        }

        public async Task<Paging<CurrencyModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Currency>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.CurrencyName.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Currency, CurrencyModel>(_mapper);
        }

        public async Task<Paging<CurrencyModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Currency>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.CurrencyName.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Currency, CurrencyModel>(_mapper);
        }

        public async Task<CurrencyModel> UpdateCurrencyDetailsAsync(long currencyId, CurrencyModel model)
        {
            var entity = _mapper.Map<CurrencyModel, Currency>(model);
            await _unitOfWork.Repository<Currency>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CurrencyModel();
        }

        public async Task<CurrencyModel> UpdateCurrencyDetailsAsync(long currencyId, string model)
        {
            var currency = JsonConvert.DeserializeObject<CurrencyModel>(model);

            var entity = _mapper.Map<CurrencyModel, Currency>(currency);
            await _unitOfWork.Repository<Currency>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CurrencyModel();
        }
    }
}
