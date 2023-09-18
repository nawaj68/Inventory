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
    public class DamageService : BaseService<Damage>, IDamageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DamageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<DamageModel> AddDamageDetailsAsync(DamageModel model)
        {
            var entity = _mapper.Map<DamageModel, Damage>(model);
            await _unitOfWork.Repository<Damage>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new DamageModel();
        }

        public async Task<DamageModel> GetDamageDetailsAsync(long damageId)
        {
            var data = await _unitOfWork.Repository<Damage>().FirstOrDefaultAsync(f => f.Id == damageId);
            return _mapper.Map<Damage, DamageModel>(data);
        }

        public async Task<Dropdown<DamageModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.pageSize)
        {
            var data = await _unitOfWork.Repository<Damage>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.DamageDate.ToString().Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new DamageModel { Id = se.Id, DamageDate = se.DamageDate },
                 size);
            return data;
        }

        public async Task<Paging<DamageModel>> GetFilterAsync(int pageIndex = 0, int pageSize = 10, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Damage>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.DamageDate.ToString().Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Damage, DamageModel>(_mapper);
        }

        public async Task<Paging<DamageModel>> GetSearchAsync(int pageIndex = 0, int pageSize = 10, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Damage>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.DamageDate.ToString().Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se,i=>i.Item,
                i=>i.Company);
            return data.ToPagingModel<Damage, DamageModel>(_mapper);
        }

        public async Task<DamageModel> UpdateDamageDetailsAsync(long damageId, DamageModel model)
        {
            var entity = _mapper.Map<DamageModel, Damage>(model);
            await _unitOfWork.Repository<Damage>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new DamageModel();
        }

        //public async Task<DamageModel> UpdateDamageDetailsAsync(long damageId, string model)
        //{
        //    var city = JsonConvert.DeserializeObject<CompanyModel>(model);

        //    var entity = _mapper.Map<DamageModel, Damage>(city);
        //    await _unitOfWork.Repository<Damage>().UpdateAsync(entity);
        //    await _unitOfWork.CompleteAsync();

        //    return new DamageModel();
        //}
    }
}
