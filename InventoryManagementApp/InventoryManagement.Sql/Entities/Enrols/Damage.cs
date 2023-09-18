using InventoryManagement.Sql.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Sql.Entities.Enrols
{
    public class Damage: BaseEntity
    {
        public long? ItemId { get; set; }
        public long? CompanyId { get; set; }
        public double Quantity { get; set; }
        public double DamageQuantity { get; set; }
        public string DamageReason { get; set; }
        public DateTime DamageDate { get; set; }

        public Item Item { get; set; }
        public Company Company { get; set; }

    }
}
