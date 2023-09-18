using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class City:BaseEntity
    {
        public long StateId { get; set; }
        public string Name { get; set; }

        public State State { get; set; }

        public IList<Company> Companies { get; set; }
        public IList<Customer> Customers { get; set; }

    }
}
