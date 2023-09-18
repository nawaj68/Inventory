using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Core.Collections;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Sql.Entities.Configurations;
using MassTransit.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InventoryManagement.Service.Services.Configurations
{
    public class CategoryService : BaseService<Category>,ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CategoryModel> AddCategoryDetailsAsync(CategoryModel model)
        {
            string uniqueFileName = string.Empty;
            if (model.PictureFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, CommonVariables.AvatarLocation);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PictureFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PictureFile.CopyTo(fileStream);
                }

                model.Picture = uniqueFileName;
            }
            var entity = _mapper.Map<CategoryModel, Category>(model);
            await _unitOfWork.Repository<Category>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();
            return new CategoryModel();
        }

        public async Task<CategoryModel> GetCategoryDetailsAsync(long categoryId)
        {
            var data = await _unitOfWork.Repository<Category>().FirstOrDefaultAsync(f=>f.Id== categoryId);
            return _mapper.Map<Category, CategoryModel>(data);
        }

        public async Task<Dropdown<CategoryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<Category>().GetDropdownAsync(
                p=>(string.IsNullOrEmpty(searchText)|p.CategoryName.Contains(searchText)),
                o=>o.OrderBy(ob=>ob.Id),
                se=> new CategoryModel { Id=se.Id, CategoryName=se.CategoryName, CategoryCode=se.CategoryCode },
                size);
            return data;
        }

        public async Task<Paging<CategoryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<Category>().GetPageAsync(pageIndex, pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.CategoryName.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se=>se);
            return data.ToPagingModel<Category, CategoryModel>(_mapper);
        }

        public async Task<Paging<CategoryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<Category>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.CategoryName.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);
            return data.ToPagingModel<Category, CategoryModel>(_mapper);
        }

        public async Task<CategoryModel> UpdateCategoryDetailsAsync(long categoryId, CategoryModel model)
        {
            string uniqueFileName = string.Empty;
            if (model.PictureFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, CommonVariables.AvatarLocation);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PictureFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PictureFile.CopyTo(fileStream);
                }

                model.Picture = uniqueFileName;
            }
            else
            {
                model.Picture = model.Picture?.Split("/")?.LastOrDefault();
            }


            var entity = _mapper.Map<CategoryModel, Category>(model);

            await _unitOfWork.Repository<Category>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CategoryModel();
        }

        public async Task<CategoryModel> UpdateCategoryDetailsAsync(long categoryId, string model, List<IFormFile> images)
        {
            var image = images.FirstOrDefault();
            var category = JsonConvert.DeserializeObject<CategoryModel>(model);
            string uniqueFileName = string.Empty;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, CommonVariables.AvatarLocation);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            category.Picture = uniqueFileName;

            var entity = _mapper.Map<CategoryModel, Category>(category);

            await _unitOfWork.Repository<Category>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new CategoryModel();
        }
    }
}
