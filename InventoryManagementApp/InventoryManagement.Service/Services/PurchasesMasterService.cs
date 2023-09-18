using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services
{
    public class PurchasesMasterService : BaseService<PurchasesMaster>, IPurchasesMasterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PurchasesMasterService(IUnitOfWork unitOfWork,
      IMapper mapper,
      IWebHostEnvironment webHostEnvironment,
      IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Paging<PurchasesMasterModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<PurchasesMaster>().GetPageAsync(pageIndex,
                pageSize,
                s => (string.IsNullOrEmpty(searchText) || s.PurchasesCode.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                p => p.Supplier,
                q => q.Company
                );

            var response = data.ToPagingModel<PurchasesMaster, PurchasesMasterModel>(_mapper);

            return response;
        }
        public async Task<Paging<PurchasesMasterModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null)
        {
            var data = await _unitOfWork.Repository<PurchasesMaster>().GetPageAsync(pageIndex,
                pageSize,
                s => ((string.IsNullOrEmpty(filterText1) || s.PurchasesCode.Contains(filterText1))),

                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                 p => p.Supplier,
                q => q.Company
                );

            var response = data.ToPagingModel<PurchasesMaster, PurchasesMasterModel>(_mapper);

            return response;
        }
        public async Task<PurchasesMasterModel> GetPurchasesMasterDetailAsync(long purchasesmasterId)
        {
            var data = await _unitOfWork.Repository<PurchasesMaster>().FirstOrDefaultAsync(f => f.Id == purchasesmasterId,
                o => o.OrderBy(ob => ob.Id),
                i => i.User,
                 p => p.Supplier,
                q => q.Company
                );


            var response = _mapper.Map<PurchasesMaster, PurchasesMasterModel>(data);

            return response;
        }
        public async Task<PurchasesMasterModel> AddPurchasesMasterDetailAsync(PurchasesMasterModel purchasesmaster)
        {


            var entity = _mapper.Map<PurchasesMasterModel, PurchasesMaster>(purchasesmaster);

            await _unitOfWork.Repository<PurchasesMaster>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchasesMasterModel();
        }
        public async Task<PurchasesMasterModel> UpdatePurchasesMasterDetailAsync(long purchasesmasterId, PurchasesMasterModel purchasesmaster)
        {
            var entity = _mapper.Map<PurchasesMasterModel, PurchasesMaster>(purchasesmaster);

            await _unitOfWork.Repository<PurchasesMaster>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchasesMasterModel();
        }
        public async Task<PurchasesMasterModel> UpdatePurchasesMasterDetailAsync(long purchasesmasterId, string model)
        {
            var purchasesmaster = JsonConvert.DeserializeObject<PurchasesMasterModel>(model);
            var entity = _mapper.Map<PurchasesMasterModel, PurchasesMaster>(purchasesmaster);

            await _unitOfWork.Repository<PurchasesMaster>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchasesMasterModel();
        }
        public async Task<Dropdown<PurchasesMasterModel>> GetDropdownAsync(string searchText = null,
          int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<PurchasesMaster>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.PurchasesCode.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new PurchasesMasterModel { Id = se.Id, PurchasesCode = se.PurchasesCode },
                size);

            return data;
        }
    }
}
