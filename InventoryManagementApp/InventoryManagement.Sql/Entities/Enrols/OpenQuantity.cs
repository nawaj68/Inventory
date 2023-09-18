using InventoryManagement.Sql.Entities.Base;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class OpenQuantity:BaseEntity
    {
        public long ItemId { get; set; }
        public double OpeningQuentity { get; set; }
        public double PurchaseQuantity { get;set; }
        public string Sell { get; set; }
        public double ReorderQuantity { get; set;}

        public Item Item { get; set; }
    }
}
