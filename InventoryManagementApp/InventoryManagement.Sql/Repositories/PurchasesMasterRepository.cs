using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class PurchasesMasterRepository : SqlRepository<PurchasesMaster>, IPurchasesMasterRepository
    {
        public PurchasesMasterRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
