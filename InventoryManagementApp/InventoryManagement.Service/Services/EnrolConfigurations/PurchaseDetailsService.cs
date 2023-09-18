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

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class PurchaseDetailsService : BaseService<PurchaseDetails>, IPurchaseDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PurchaseDetailsService(IUnitOfWork unitOfWork,
      IMapper mapper,
      IWebHostEnvironment webHostEnvironment,
      IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Paging<PurchaseDetailsModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<PurchaseDetails>().GetPageAsync(pageIndex,
                pageSize,
                s => (string.IsNullOrEmpty(searchText) || s.batchNumber.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                p => p.PurchasesMaster,
                q => q.Item
                );

            var response = data.ToPagingModel<PurchaseDetails, PurchaseDetailsModel>(_mapper);

            return response;
        }
        public async Task<Paging<PurchaseDetailsModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null)
        {
            var data = await _unitOfWork.Repository<PurchaseDetails>().GetPageAsync(pageIndex,
                pageSize,
                s => ((string.IsNullOrEmpty(filterText1) || s.batchNumber.Contains(filterText1))),

                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                 p => p.PurchasesMaster,
                q => q.Item
                );

            var response = data.ToPagingModel<PurchaseDetails, PurchaseDetailsModel>(_mapper);

            return response;
        }
        public async Task<PurchaseDetailsModel> GetPurchaseDetailsDetailAsync(long purchaseDetailsId)
        {
            var data = await _unitOfWork.Repository<PurchaseDetails>().FirstOrDefaultAsync(f => f.Id == purchaseDetailsId,
                o => o.OrderBy(ob => ob.Id),
                i => i.User,
                 p => p.PurchasesMaster,
                q => q.Item
                );


            var response = _mapper.Map<PurchaseDetails, PurchaseDetailsModel>(data);

            return response;
        }
        public async Task<PurchaseDetailsModel> AddPurchaseDetailsDetailAsync(PurchaseDetailsModel purchaseDetails)
        {


            var entity = _mapper.Map<PurchaseDetailsModel, PurchaseDetails>(purchaseDetails);

            await _unitOfWork.Repository<PurchaseDetails>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchaseDetailsModel();
        }
        public async Task<PurchaseDetailsModel> UpdatePurchaseDetailsDetailAsync(long purchaseDetailsId, PurchaseDetailsModel purchaseDetails)
        {
            var entity = _mapper.Map<PurchaseDetailsModel, PurchaseDetails>(purchaseDetails);

            await _unitOfWork.Repository<PurchaseDetails>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchaseDetailsModel();
        }
        public async Task<PurchaseDetailsModel> UpdatePurchaseDetailsDetailAsync(long purchaseDetailsId, string model)
        {
            var purchaseDetails = JsonConvert.DeserializeObject<PurchaseDetailsModel>(model);
            var entity = _mapper.Map<PurchaseDetailsModel, PurchaseDetails>(purchaseDetails);

            await _unitOfWork.Repository<PurchaseDetails>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new PurchaseDetailsModel();
        }
        public async Task<Dropdown<PurchaseDetailsModel>> GetDropdownAsync(string searchText = null,
          int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<PurchaseDetails>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.batchNumber.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new PurchaseDetailsModel { Id = se.Id, batchNumber = se.batchNumber },
                size);

            return data;
        }
    }
}
