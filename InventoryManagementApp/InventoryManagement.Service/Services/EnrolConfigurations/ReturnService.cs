using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities;
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
    public class ReturnService : BaseService<Return>, IReturnService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReturnService(IUnitOfWork unitOfWork,
      IMapper mapper,
      IWebHostEnvironment webHostEnvironment,
      IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Paging<ReturnModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Return>().GetPageAsync(pageIndex,
                pageSize,
                s => (string.IsNullOrEmpty(searchText) || s.reasonOfReturn.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                p => p.Company,
                q => q.Item
                );

            var response = data.ToPagingModel<Return, ReturnModel>(_mapper);

            return response;
        }
        public async Task<Paging<ReturnModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null)
        {
            var data = await _unitOfWork.Repository<Return>().GetPageAsync(pageIndex,
                pageSize,
                s => ((string.IsNullOrEmpty(filterText1) || s.reasonOfReturn.Contains(filterText1))),

                o => o.OrderBy(ob => ob.Id),
                se => se,
                i => i.User,
                 p => p.Company,
                q => q.Item
                );

            var response = data.ToPagingModel<Return, ReturnModel>(_mapper);

            return response;
        }
        public async Task<ReturnModel> GetReturnDetailAsync(long returnId)
        {
            var data = await _unitOfWork.Repository<Return>().FirstOrDefaultAsync(f => f.Id == returnId,
                o => o.OrderBy(ob => ob.Id),
                i => i.User,
                 p => p.Company,
                q => q.Item
                );


            var response = _mapper.Map<Return, ReturnModel>(data);

            return response;
        }
        public async Task<ReturnModel> AddReturnDetailAsync(ReturnModel returns)
        {


            var entity = _mapper.Map<ReturnModel, Return>(returns);

            await _unitOfWork.Repository<Return>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ReturnModel();
        }
        public async Task<ReturnModel> UpdateReturnDetailAsync(long returnId, ReturnModel returns)
        {
            var entity = _mapper.Map<ReturnModel, Return>(returns);

            await _unitOfWork.Repository<Return>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ReturnModel();
        }
        public async Task<ReturnModel> UpdateReturnDetailAsync(long returnId, string model)
        {
            var returns = JsonConvert.DeserializeObject<ReturnModel>(model);
            var entity = _mapper.Map<ReturnModel, Return>(returns);

            await _unitOfWork.Repository<Return>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ReturnModel();
        }
        public async Task<Dropdown<ReturnModel>> GetDropdownAsync(string searchText = null,
          int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Return>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) || p.reasonOfReturn.Contains(searchText)),
                o => o.OrderBy(ob => ob.Id),
                se => new ReturnModel { Id = se.Id, reasonOfReturn = se.reasonOfReturn },
                size);

            return data;
        }
    }
}
