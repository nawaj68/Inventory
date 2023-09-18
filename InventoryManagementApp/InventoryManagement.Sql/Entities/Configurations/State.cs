using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class State:BaseEntity
    {
        public long CountryId { get; set; }
        public string Name { get; set; }

        public Country Country { get; set; }

        public IList<City> Cities { get; set; }
        public IList<Company> Companies { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}
