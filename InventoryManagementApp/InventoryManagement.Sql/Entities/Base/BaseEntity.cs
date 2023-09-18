using InventoryManagement.Core.Sqls;

namespace InventoryManagement.Sql.Entities.Base
{
    public class BaseEntity: MasterEntity
    {
        public long CreatedBy { get; set; }
        public DateTimeOffset CreatedDateUtc { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDateUtc { get; set; }
    }
}
