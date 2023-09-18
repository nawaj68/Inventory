using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Service.Models.Enrols
{
    public class ReturnModel : MasterModel
    {
        public ReturnModel()
        {
        }
        public long? UserId { get; set; }
        public long ReturnId { get; set; }
        public long ItemId { get; set; }
        public double quantity { get; set; }
        public string reasonOfReturn { get; set; }
        public DateTime returnDate { get; set; }
        public long CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public ItemModel Item { get; set; }
        public UserModel User { get; set; }
    }
}
