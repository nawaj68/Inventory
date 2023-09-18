using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<CustomerModel> AddCustomerDetailsAsync(CustomerModel model)
        {
            var entity = _mapper.Map<CustomerModel, Customer>(model);
            await _unitOfWork.Repository<Customer>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CustomerModel();
        }

        public async Task<CustomerModel> GetCustomerDetailsAsync(long customerId)
        {
            var data = await _unitOfWork.Repository<Customer>().FirstOrDefaultAsync(f => f.Id == customerId);
            return _mapper.Map<Customer, CustomerModel>(data);
        }

        public async Task<Dropdown<CustomerModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Customer>().GetDropdownAsync(
                 p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
                 o => o.OrderBy(ob => ob.Id),
                 se => new CustomerModel { Id = se.Id, Name = se.Name },
                 size);
            return data;
        }

        public async Task<Paging<CustomerModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Customer>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.Name.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Customer, CustomerModel>(_mapper);
        }

        public async Task<Paging<CustomerModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Customer>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.Name.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Customer, CustomerModel>(_mapper);
        }

        public async Task<CustomerModel> UpdateCustomerDetailsAsync(long customerId, CustomerModel model)
        {
            var entity = _mapper.Map<CustomerModel, Customer>(model);
            await _unitOfWork.Repository<Customer>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CustomerModel();
        }

        public async Task<CustomerModel> UpdateCustomerDetailsAsync(long customerId, string model)
        {
            var customer = JsonConvert.DeserializeObject<CustomerModel>(model);

            var entity = _mapper.Map<CustomerModel, Customer>(customer);
            await _unitOfWork.Repository<Customer>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CustomerModel();
        }
    }
}
