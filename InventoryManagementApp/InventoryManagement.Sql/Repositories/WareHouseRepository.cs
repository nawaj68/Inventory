using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class WareHouseRepository:SqlRepository<WareHouse>,IWareHouseRepository
    {
        public WareHouseRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
