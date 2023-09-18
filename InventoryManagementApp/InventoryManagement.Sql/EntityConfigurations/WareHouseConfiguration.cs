using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class WareHouseConfiguration: IEntityTypeConfiguration<WareHouse>
    {
        public void Configure(EntityTypeBuilder<WareHouse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Company).WithMany(m => m.WareHouses).HasForeignKey(f => f.CompanyId);
            builder.HasOne(x => x.Country).WithMany(m => m.WareHouses).HasForeignKey(f => f.CountryId);
        }
    }
}
