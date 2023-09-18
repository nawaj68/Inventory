using InventoryManagement.Service.Models.Configurations;
namespace InventoryManagement.Service.Models.Enrols
{
    public class PurchasesMasterModel :MasterModel
    {
        public PurchasesMasterModel()
        {
        }
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
        public CompanyModel Company { get; set; }
        public UserModel User { get; set; }
        public SupplierModel Supplier { get; set; }
    }
}
