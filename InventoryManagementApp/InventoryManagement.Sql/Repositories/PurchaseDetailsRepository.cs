using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Sql.Repositories
{
    public class PurchaseDetailsRepository : SqlRepository<PurchaseDetails>, IPurchaseDetailsRepository
    {
        public PurchaseDetailsRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
