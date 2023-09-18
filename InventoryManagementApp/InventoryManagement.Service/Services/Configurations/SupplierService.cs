using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class SupplierService : BaseService<Supplier>,ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SupplierService(IUnitOfWork unitOfWork,
      IMapper mapper,
      IWebHostEnvironment webHostEnvironment,
      IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Paging<SupplierModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Supplier>().GetPageAsync(pageIndex,
                pageSize,
                s => (string.IsNullOrEmpty(searchText) || s.SupplierName.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);

            var response = data.ToPagingModel<Supplier, SupplierModel>(_mapper);

            return response;
        }
        public async Task<Paging<SupplierModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null)
        {
            var data = await _unitOfWork.Repository<Supplier>().GetPageAsync(pageIndex,
                pageSize,
                s => ((string.IsNullOrEmpty(filterText1) || s.SupplierName.Contains(filterText1))),

                o => o.OrderBy(ob => ob.Id),
                se => se);

            var response = data.ToPagingModel<Supplier, SupplierModel>(_mapper);
            return response;
        }
        public async Task<SupplierModel> GetSupplierDetailAsync(long supplierId)
        {
            var data = await _unitOfWork.Repository<Supplier>().FirstOrDefaultAsync(f => f.Id == supplierId,
                o => o.OrderBy(ob => ob.Id));


            var response = _mapper.Map<Supplier, SupplierModel>(data);

            return response;
        }
        public async Task<SupplierModel> AddSupplierDetailAsync(SupplierModel supplier)
        {


            var entity = _mapper.Map<SupplierModel, Supplier>(supplier);

            await _unitOfWork.Repository<Supplier>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SupplierModel();
        }
        public async Task<SupplierModel> UpdateSupplierDetailAsync(long supplierId, SupplierModel supplier)
        {
            var entity = _mapper.Map<SupplierModel, Supplier>(supplier);

            await _unitOfWork.Repository<Supplier>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SupplierModel();
        }
        public async Task<SupplierModel> UpdateSupplierDetailAsync(long supplierId, string model)
        {
            var supplier = JsonConvert.DeserializeObject<SupplierModel>(model);
            var entity = _mapper.Map<SupplierModel, Supplier>(supplier);

            await _unitOfWork.Repository<Supplier>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SupplierModel();
        }
        public async Task<Dropdown<SupplierModel>> GetDropdownAsync(string searchText = null,
          int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Supplier>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.SupplierName.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new SupplierModel { Id = se.Id, SupplierName = se.SupplierName },
                size);

            return data;
        }

    }
}
