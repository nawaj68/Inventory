
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Service.Models.Enrols
{
    public class ItemModel:MasterModel
    {
        public long SubCategoryId { get; set; }
        public long CompanyId { get; set; }
        public long UnitId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string Measure { get; set; }
        public double Measurevalue { get; set; }
        public double UnitPrice { get; set; }
        public double SellPrice { get; set; }
        public double OldUnitPrice { get; set; }
        public double OldSellPrice { get; set; }
        public string ReOrderLevel { get; set; }
        public double Stock { get; set; }

        public SubCategoryModel SubCategory { get; set; }
        public CompanyModel Company { get; set; }
        public UnitModel Unit { get; set; }
    }
}
