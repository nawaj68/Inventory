using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Currency : BaseEntity
    {
        public long CountryId { get; set; }
        public long CompanyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string Syambol { get; set; }
        public bool IsDefault { get; set; }

        public Country Country { get; set; }
        public Company Company { get; set; }

    }
}
