using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Country).WithMany(m => m.Currencies).HasForeignKey(f => f.CountryId);
            builder.HasOne(x => x.Company).WithMany(m => m.Currencies).HasForeignKey(f => f.CompanyId);
        }
    }
}
