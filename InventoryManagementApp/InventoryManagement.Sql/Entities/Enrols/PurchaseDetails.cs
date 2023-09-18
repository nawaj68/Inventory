using InventoryManagement.Sql.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class PurchaseDetails : BaseEntity
    {
        public long? UserId { get; set; }
        public long purchasesMasterId { get; set; }
        public long ItemId { get; set; }
        public double quantity { get; set; }
        public double unitePrice { get; set; }
        public double sellPrice { get; set; }
        public string batchNumber { get; set; }

        public PurchasesMaster PurchasesMaster { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
    }
}
