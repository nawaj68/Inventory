using InventoryManagement.Sql.Entities.Base;

namespace InventoryManagement.Sql.Entities.Configurations
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public Boolean Cancel { get; set; }

        public IList<SubCategory> SubCategories { get; set; }
    }
}
