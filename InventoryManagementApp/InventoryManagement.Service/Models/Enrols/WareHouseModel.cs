using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Enrols
{
    public class WareHouseModel:MasterModel
    {
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
        public CountryModel Country { get; set; }
    }
}
