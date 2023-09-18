using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Enrols;
using MassTransit.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.EnrolConfigurations
{
    public class ItemService : BaseService<Item>, IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<ItemModel> AddItemDetailsAsync(ItemModel model)
        {
            var entity = _mapper.Map<ItemModel, Item>(model);
            await _unitOfWork.Repository<Item>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ItemModel();
        }

        public async Task<Dropdown<ItemModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Item>().GetDropdownAsync(
                p=>(string.IsNullOrEmpty(searchText)|p.ItemName.Contains(searchText)|p.ItemCode.Contains(searchText)),
                o=>o.OrderBy(o=>o.Id),
                se=> new ItemModel { Id = se.Id, ItemName = se.ItemName, ItemCode=se.ItemCode},
                size);

            return data;
        }

        public async Task<Paging<ItemModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Item>().GetPageAsync(pageIndex,pageSize,
               p => (string.IsNullOrEmpty(filterText) | p.ItemName.Contains(filterText) | p.ItemCode.Contains(filterText)),
               o => o.OrderBy(o => o.Id),
               se => se);

            return data.ToPagingModel<Item, ItemModel>(_mapper);
        }

        public async Task<ItemModel> GetItemDetailsAsync(long itemId)
        {
            var data = await _unitOfWork.Repository<Item>().FirstOrDefaultAsync(f=>f.Id== itemId);
            return _mapper.Map<Item, ItemModel>(data);  
        }

        public async Task<Paging<ItemModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Item>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.ItemName.Contains(searchText) | p.ItemCode.Contains(searchText)),
            o => o.OrderBy(o => o.Id),
            se => se);
            return data.ToPagingModel<Item, ItemModel>(_mapper);
        }

        public async Task<ItemModel> UpdateItemDetailsAsync(long itemId, ItemModel model)
        {
            var entity = _mapper.Map<ItemModel, Item>(model);
            await _unitOfWork.Repository<Item>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ItemModel();
        }

        public async Task<ItemModel> UpdateItemDetailsAsync(long itemId, string model)
        {
            var item = JsonConvert.DeserializeObject<ItemModel>(model);

            var entity = _mapper.Map<ItemModel, Item>(item);
            await _unitOfWork.Repository<Item>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ItemModel();
        }
    }
}
