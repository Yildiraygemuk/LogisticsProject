using Logistics.DataAccess.Concrete.EntityFramework.Configuration;
using Logistics.DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Logistics.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.DataAccess
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public override void EntityConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.ProductCode).NVarChar(250);
            builder.Property(x => x.OrderNo).NVarChar(250);
            builder.Property(x => x.FromAddress).NVarChar(1500);
            builder.Property(x => x.GoingAddress).NVarChar(1500);
            builder.Property(x => x.Note).NVarChar(1500);

            builder.HasOne(x => x.Product).
               WithMany(x => x.Orders)
               .HasForeignKey(x => x.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
