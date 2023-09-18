using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;
using InventoryManagement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Configurations
{
    public class SubCategoryModel: MasterModel
    {
        public long CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryCode { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public IFormFile PictureFile { get; set; }
        public Boolean Cancel { get; set; }

        public CategoryModel Category { get; set; }
    }
}
