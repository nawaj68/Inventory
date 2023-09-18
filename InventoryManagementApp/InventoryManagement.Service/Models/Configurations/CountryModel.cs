using InventoryManagement.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Configurations
{
    public class CountryModel:MasterModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public string Flag { get; set; }
    }
}
