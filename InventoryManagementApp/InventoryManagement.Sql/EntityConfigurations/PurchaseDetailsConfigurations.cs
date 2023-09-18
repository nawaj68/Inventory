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
    public class PurchaseDetailsConfigurations : IEntityTypeConfiguration<PurchaseDetails>
    {
        public void Configure(EntityTypeBuilder<PurchaseDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Item).WithMany(m => m.PurchaseDetails).HasForeignKey(f => f.ItemId);
            builder.HasOne(x => x.PurchasesMaster).WithMany(m => m.PurchaseDetails).HasForeignKey(f => f.ItemId);
            builder.HasOne(x => x.User).WithMany(m => m.PurchaseDetails).HasForeignKey(f => f.UserId);
           

        }
    }
}
