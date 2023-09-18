using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Item:BaseEntity
    {
        public long SubCategoryId { get; set; }
        public long CompanyId { get; set; }
        public long UnitId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Measure { get; set; }
        public double? Measurevalue { get; set; }
        public double UnitPrice { get; set; }
        public double SellPrice { get; set; }
        public double? OldUnitPrice { get; set; }
        public double? OldSellPrice { get; set; }
        public string ReOrderLevel { get; set; }
        public double? Stock { get; set; }

        public SubCategory SubCategory { get; set; }
        public Company Company { get; set; }
        public Unit Unit { get; set; }


        public IList<OpenQuantity> OpenQuentities { get; set; }
        public IList<PurchaseDetails> PurchaseDetails { get; set; }
        public IList<Return> Returns { get; set; }
        public IList<Damage> Damages { get; set; }
    }
}
