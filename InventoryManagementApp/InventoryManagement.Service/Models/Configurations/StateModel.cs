using InventoryManagement.Sql.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Configurations
{
    public class StateModel:MasterModel
    {
        public long CountryId { get; set; }
        public string Name { get; set; }

        public CountryModel Country { get; set; }
    }
}
