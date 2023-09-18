using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x=>x.Country).WithMany(m=>m.Customers).HasForeignKey(x=>x.CountryId);
            builder.HasOne(x=>x.State).WithMany(m=>m.Customers).HasForeignKey(x=>x.StateId);
            builder.HasOne(x=>x.City).WithMany(m=>m.Customers).HasForeignKey(x=>x.CityId);
        }
    }
}
