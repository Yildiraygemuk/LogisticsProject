using Logistics.Core.DataAccess.EntityFramework.Abstract;
using Logistics.Entity;

namespace Logistics.DataAccess.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}
