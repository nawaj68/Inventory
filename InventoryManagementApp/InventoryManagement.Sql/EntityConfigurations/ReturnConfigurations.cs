using InventoryManagement.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Sql.EntityConfigurations
{
    public class ReturnConfigurations : IEntityTypeConfiguration<Return>
    {
        public void Configure(EntityTypeBuilder<Return> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Company).WithMany(m => m.Returns).HasForeignKey(f => f.CompanyId);
            builder.HasOne(x => x.Item).WithMany(m => m.Returns).HasForeignKey(f => f.ItemId);
            builder.HasOne(x => x.User).WithMany(m => m.Returns).HasForeignKey(f => f.UserId);


        }
    }
}
