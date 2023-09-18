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
    public class DamageConfiguration: IEntityTypeConfiguration<Damage>
    {
        public void Configure(EntityTypeBuilder<Damage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Company).WithMany(m => m.Damages).HasForeignKey(f => f.CompanyId);
            builder.HasOne(x => x.Item).WithMany(m => m.Damages).HasForeignKey(f => f.ItemId);
        }
    }
}
