using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x=>x.Company).WithMany(m=>m.Items).HasForeignKey(x=>x.CompanyId);
            builder.HasOne(x=>x.SubCategory).WithMany(m=>m.Items).HasForeignKey(x=>x.SubCategoryId);
            builder.HasOne(x=>x.Unit).WithMany(m=>m.Items).HasForeignKey(x=>x.UnitId);
        }
    }
}
