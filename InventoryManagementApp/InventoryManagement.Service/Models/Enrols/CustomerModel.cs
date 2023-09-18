using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Enrols
{
    public class CustomerModel:MasterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }

        public CountryModel Country { get; set; }
        public StateModel State { get; set; }
        public CityModel City { get; set; }
    }
}
