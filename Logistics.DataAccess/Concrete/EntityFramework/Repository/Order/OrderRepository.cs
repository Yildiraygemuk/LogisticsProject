using Logistics.Core.DataAccess.EntityFramework;
using Logistics.DataAccess.Abstract;
using Logistics.DataAccess.Concrete;
using Logistics.Entity;

namespace Logistics.DataAccess
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LogisticsContext context) : base(context)
        {
        }
    }
}
