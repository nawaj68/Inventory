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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Configurations
{
    public class SubCategoryService : BaseService<SubCategory>, ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<SubCategoryModel> AddSubCategoryDetailsAsync(SubCategoryModel model)
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

            var entity = _mapper.Map<SubCategoryModel, SubCategory>(model);
            await _unitOfWork.Repository<SubCategory>().InsertAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SubCategoryModel();
        }

        public async Task<Dropdown<SubCategoryModel>> GetDropdownAsync(string searchText = null, int size = CommonVariables.DropdownSize)
        {
            var data = await _unitOfWork.Repository<SubCategory>().GetDropdownAsync(
                p => (string.IsNullOrEmpty(searchText) | p.SubCategoryName.Contains(searchText)),
                o=>o.OrderBy(ob=>ob.Id),
                se=> new SubCategoryModel { Id = se.Id, SubCategoryName=se.SubCategoryName,SubCategoryCode =se.SubCategoryCode },
                size);
            return data;
        }

        public async Task<Paging<SubCategoryModel>> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText = null)
        {
            var data = await _unitOfWork.Repository<SubCategory>().GetPageAsync(pageIndex,pageSize,
                p => (string.IsNullOrEmpty(filterText) | p.SubCategoryName.Contains(filterText)),
                o => o.OrderBy(ob => ob.Id),
                se => se);

            return data.ToPagingModel<SubCategory, SubCategoryModel>(_mapper);
        }

        public async Task<Paging<SubCategoryModel>> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var data = await _unitOfWork.Repository<SubCategory>().GetPageAsync(pageIndex, pageSize,
            p => (string.IsNullOrEmpty(searchText) | p.SubCategoryName.Contains(searchText)),
            o => o.OrderBy(ob => ob.Id),
                se => se);

            return data.ToPagingModel<SubCategory, SubCategoryModel>(_mapper);
        }

        public async Task<SubCategoryModel> GetSubCategoryDetailsAsync(long subCategoryId)
        {
            var data = await _unitOfWork.Repository<SubCategory>().FirstOrDefaultAsync(f=> f.Id == subCategoryId);
            return _mapper.Map<SubCategory, SubCategoryModel>(data);
        }

        public async Task<SubCategoryModel> UpdateSubCategoryDetailsAsync(long subCategoryId, SubCategoryModel model)
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

            var entity = _mapper.Map<SubCategoryModel, SubCategory>(model);
            await _unitOfWork.Repository<SubCategory>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SubCategoryModel();
        }

        public async Task<SubCategoryModel> UpdateSubCategoryDetailsAsync(long subCategoryId, string model, List<IFormFile> images)
        {
            var image = images.FirstOrDefault();
            var subCategory = JsonConvert.DeserializeObject<SubCategoryModel>(model);
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

            subCategory.Picture = uniqueFileName;

            var entity = _mapper.Map<SubCategoryModel, SubCategory>(subCategory);
            await _unitOfWork.Repository<SubCategory>().UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new SubCategoryModel();
        }
    }
}
