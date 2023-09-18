using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Configurations
{
    public class CityModel:MasterModel
    {
        public long StateId { get; set; }
        public string Name { get; set; }

        public StateModel State { get; set; }
    }
}
