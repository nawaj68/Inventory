using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }

        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }
        public IList<Sales> Sales { get; set; }

    }
}
