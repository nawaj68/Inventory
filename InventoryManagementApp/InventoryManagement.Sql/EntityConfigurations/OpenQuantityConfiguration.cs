using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class OpenQuantityConfiguration : IEntityTypeConfiguration<OpenQuantity>
    {
        public void Configure(EntityTypeBuilder<OpenQuantity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x=>x.Item).WithMany(m=>m.OpenQuentities).HasForeignKey(x=>x.ItemId);
        }
    }
}
