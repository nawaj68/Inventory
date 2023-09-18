using InventoryManagement.Core.Sqls;
using InventoryManagement.Sql.DbDependencies;
using InventoryManagement.Sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Sql.Repositories
{
    public class ReturnRepository : SqlRepository<Return>, IReturnRepository
    {
        public ReturnRepository(WebAppContext Context) : base(Context)
        {
        }
    }
}
