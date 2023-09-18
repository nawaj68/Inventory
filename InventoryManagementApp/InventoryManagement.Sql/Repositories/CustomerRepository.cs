using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class CustomerRepository : SqlRepository<Customer>
    {
        public CustomerRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
