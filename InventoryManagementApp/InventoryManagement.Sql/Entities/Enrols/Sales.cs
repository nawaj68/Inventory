using InventoryManagement.Sql.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Sales : BaseEntity
    {
        public long? UserId { get; set; }
        public long? CompanyId { get; set; }
        public DateTime salesDate { get; set; }
        public long? CustomerId { get; set; }
        public string chalanNumber { get; set; }
        public DateTime chalanDate { get; set; }
        public string selesType { get; set; }
        public string salesCode { get; set; }
        public string mrNumber { get; set; }
        public string warranty { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public string customerPhone { get; set; }
        public string vatRate { get; set; }
        public double vatAmount { get; set; }
        public double discount { get; set; }
        public double cashAmount { get; set; }
        public double cardAmount { get; set; }
        public double dueAmount { get; set; }
        public long? CardId { get; set; }
        public string cardRate { get; set; }
        public double cardDiscount { get; set; }
        public double cardDiscountAmount { get; set; }
        public double cardPaymentAmount { get; set; }
        public string discountRefference { get; set; }
        public double groundTotal { get; set; }
        public double taxRate { get; set; }
        public double taxAmount { get; set; }
        public User User { get; set; }
        public Company Company { get; set; }
        public Customer Customer { get; set; }
        
    }
}
