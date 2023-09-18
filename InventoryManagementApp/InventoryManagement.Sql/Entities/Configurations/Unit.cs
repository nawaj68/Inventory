using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class Unit:BaseEntity
    {
        public string UnitName { get; set; }

        public IList<Item> Items { get; set; }
    }
}
