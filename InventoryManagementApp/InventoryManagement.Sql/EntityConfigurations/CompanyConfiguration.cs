using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithMany(m => m.Companies).HasForeignKey(f => f.UserId);
            builder.HasOne(x => x.Country).WithMany(m => m.Companies).HasForeignKey(f => f.CountryId);
            builder.HasOne(x => x.State).WithMany(m => m.Companies).HasForeignKey(f => f.StateId);
            builder.HasOne(x => x.City).WithMany(m => m.Companies).HasForeignKey(f => f.CityId);
        }
    }
}
