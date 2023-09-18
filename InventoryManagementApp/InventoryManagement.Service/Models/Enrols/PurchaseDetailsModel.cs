using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Service.Models.Enrols
{
    public class PurchaseDetailsModel : MasterModel
    {
        public PurchaseDetailsModel()
        {
        }
        public long? UserId { get; set; }
        public long PurchaseDetailsId { get; set; }
        public long purchasesMasterId { get; set; }
        public long ItemId { get; set; }
        public double quantity { get; set; }
        public double unitePrice { get; set; }
        public double sellPrice { get; set; }
        public string batchNumber { get; set; }

        public PurchasesMasterModel PurchasesMaster { get; set; }
        public ItemModel Item { get; set; }
        public UserModel User { get; set; }
    }
}
