using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public IList<PurchasesMaster> PurchasesMasters { get; set; }
    }
}
