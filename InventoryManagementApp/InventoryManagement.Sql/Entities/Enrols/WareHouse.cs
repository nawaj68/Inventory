using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class WareHouse:BaseEntity
    {
        public long? CompanyId { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long? CountryId { get; set; }
        public Country Country { get; set; }
    }
}
