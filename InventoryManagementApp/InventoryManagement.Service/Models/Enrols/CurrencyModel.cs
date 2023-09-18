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
    public class CurrencyModel:MasterModel
    {
        public long CountryId { get; set; }
        public long CompanyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string Syambol { get; set; }
        public bool IsDefault { get; set; }

        public CountryModel Country { get; set; }
        public CompanyModel Company { get; set; }
    }
}
