using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Repositories
{
    public class CompanyRepository : SqlRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
