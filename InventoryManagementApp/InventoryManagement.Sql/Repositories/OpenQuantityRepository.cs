using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class OpenQuantityRepository : SqlRepository<OpenQuantity>,IOpenQuantityRepository
    {
        public OpenQuantityRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
