using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class Country:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public string Flag { get; set; }

        public IList<State> States { get; set; }
        public IList<Company> Companies { get; set; }
        public IList<Currency> Currencies { get; set; }
        public IList<WareHouse> WareHouses { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}
