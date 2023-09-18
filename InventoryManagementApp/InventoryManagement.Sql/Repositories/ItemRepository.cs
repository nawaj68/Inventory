using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class ItemRepository : SqlRepository<Item>, IItemRepository
    {
        public ItemRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
