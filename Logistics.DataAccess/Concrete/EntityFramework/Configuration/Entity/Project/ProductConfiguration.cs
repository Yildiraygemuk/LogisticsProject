using Logistics.DataAccess.Concrete.EntityFramework.Configuration;
using Logistics.DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Logistics.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logistics.DataAccess
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void EntityConfigure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).NVarChar(250);
        }
    }
}
