using InventoryManagement.Core.Sqls;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Configurations
{
    public class CategoryModel:MasterEntity
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public IFormFile PictureFile { get; set; }
        public Boolean Cancel { get; set; }
    }
}
