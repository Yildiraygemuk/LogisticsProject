using Logistics.Core.DataAccess.EntityFramework;
using Logistics.DataAccess.Abstract;
using Logistics.DataAccess.Concrete;
using Logistics.Entity;

namespace Logistics.DataAccess
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(LogisticsContext context) : base(context)
        {
        }
    }
}
