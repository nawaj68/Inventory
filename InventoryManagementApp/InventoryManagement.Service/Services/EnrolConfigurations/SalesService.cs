using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities;
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
    public class SalesService : BaseService<Sales>, ISalesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SalesService(IUnitOfWork unitOfWork,
      IMapper mapper,
      IWebHostEnvironment webHostEnvironment,
      IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Paging<SalesModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Sales>().GetPageAsync(pageIndex,
                pageSize,
                s => (string.IsNullOrEmpty(searchText) || s.chalanNumber.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                p => p.Company,
                q => q.Customer
                );

            var response = data.ToPagingModel<Sales, SalesModel>(_mapper);

            return response;
        }
        public async Task<Paging<SalesModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null)
        {
            var data = await _unitOfWork.Repository<Sales>().GetPageAsync(pageIndex,
                pageSize,
                s => ((string.IsNullOrEmpty(filterText1) || s.chalanNumber.Contains(filterText1))),

                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                 p => p.Company,
                q => q.Customer
                );

            var response = data.ToPagingModel<Sales, SalesModel>(_mapper);

            return response;
        }
        public async Task<SalesModel> GetSalesDetailAsync(long returnId)
        {
            var data = await _unitOfWork.Repository<Sales>().FirstOrDefaultAsync(f => f.Id == returnId,
                o => o.OrderBy(ob => ob.Id),
                i => i.User,
                 p => p.Company,
                q => q.Customer
                );


            var response = _mapper.Map<Sales, SalesModel>(data);

            return response;
        }
        public async Task<SalesModel> AddSalesDetailAsync(SalesModel sales)
        {


            var entity = _mapper.Map<SalesModel, Sales>(sales);

            await _unitOfWork.Repository<Sales>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SalesModel();
        }
        public async Task<SalesModel> UpdateSalesDetailAsync(long salesId, SalesModel sales)
        {
            var entity = _mapper.Map<SalesModel, Sales>(sales);

            await _unitOfWork.Repository<Sales>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SalesModel();
        }
        public async Task<SalesModel> UpdateSalesDetailAsync(long salesId, string model)
        {
            var returns = JsonConvert.DeserializeObject<SalesModel>(model);
            var entity = _mapper.Map<SalesModel, Sales>(returns);

            await _unitOfWork.Repository<Sales>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SalesModel();
        }
        public async Task<Dropdown<SalesModel>> GetDropdownAsync(string searchText = null,
          int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Sales>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.chalanNumber.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new SalesModel { Id = se.Id, chalanNumber = se.chalanNumber },
                size);

            return data;
        }
    }
}
