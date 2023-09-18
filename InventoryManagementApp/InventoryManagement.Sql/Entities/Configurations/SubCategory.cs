using InventoryManagement.Sql.Entities.Base;
using InventoryManagement.Sql.Entities.Enrols;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class SubCategory:BaseEntity
    {
        public long CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryCode { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public Boolean Cancel { get; set; }

        public Category Category { get; set; }

        public IList<Item> Items { get; set; }
    }
}
