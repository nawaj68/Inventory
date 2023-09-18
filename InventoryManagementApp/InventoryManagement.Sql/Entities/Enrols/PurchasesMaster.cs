using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Configurations;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class PurchasesMaster : BaseEntity
    {
        public long? UserId { get; set; }
        public DateTime PurchasesDate { get; set; }
        public string PurchasesCode { get; set; }
        public string PurchasesType { get; set; }
        public long? SupplierId { get; set; }
        public string Warrenty { get; set; }
        public string Attn { get; set; }
        public float LcNumber { get; set; }
        public DateTime LcDate { get; set; }
        public float PoNumber { get; set; }
        public string Remarks { get; set; }
        public long? CompanyId { get; set; }
        public float DiscountAmount { get; set; }
        public float DiscountPercent { get; set; }
        public float VatAmount { get; set; }
        public float VatPercent { get; set; }
        public string PaymentType { get; set; }
        public bool Cancle { get; set; }
        public Company Company { get; set; }
        public User  User { get; set; }
        public Supplier Supplier { get; set; }
        public IList<PurchaseDetails> PurchaseDetails { get; set; }


    }
}
