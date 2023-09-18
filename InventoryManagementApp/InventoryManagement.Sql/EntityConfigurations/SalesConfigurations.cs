using InventoryManagement.Sql.Entities;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class SalesConfigurations : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Company).WithMany(m => m.Sales).HasForeignKey(f => f.CompanyId);
            builder.HasOne(x => x.Customer).WithMany(m => m.Sales).HasForeignKey(f => f.CustomerId);
            builder.HasOne(x => x.User).WithMany(m => m.Sales).HasForeignKey(f => f.UserId);


        }
    }
}
