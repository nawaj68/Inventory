using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Models.Enrols
{
    public class OpenQuantityModel:MasterModel
    {
        public long ItemId { get; set; }
        public double OpeningQuentity { get; set; }
        public double PurchaseQuantity { get; set; }
        public string Sell { get; set; }
        public double ReorderQuantity { get; set; }

        public ItemModel Item { get; set; }
    }
}
