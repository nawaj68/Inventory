using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Company:BaseEntity
    {
        public long? UserId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public string ZipCode { get; set; }
        public string ContactNumber { get; set; }
        public User User { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }

        public IList<Currency> Currencies { get; set; }
        public IList<Item> Items { get; set; }
        public IList<WareHouse> WareHouses { get; set; }

        public IList<PurchasesMaster> PurchasesMasters { get;set; }
        public IList<Return> Returns { get; set; }
        public IList<Sales> Sales { get; set; }
        public IList<Damage> Damages { get; set; }
    }
}
