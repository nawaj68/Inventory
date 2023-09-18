using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.Entities
{
    public class Return : BaseEntity
    {
        public long? UserId { get; set; }
        public long ItemId { get; set; }
        public double quantity { get; set; }
        public string reasonOfReturn { get; set; }
        public DateTime returnDate { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
    }
}
