using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class CurrencyRepository : SqlRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
