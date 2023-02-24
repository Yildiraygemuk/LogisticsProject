using Logistics.Core.Utilities.Results;
using Logistics.Entity;

namespace Logistics.Business
{
    public interface IOrderService
    {
        IDataResult<IQueryable<OrderVm>> GetListQueryableOdata();
        IDataResult<OrderVm> GetById(Guid id);
        IDataResult<int> Post(OrderDto orderDto);
        IDataResult<OrderDto> Update(OrderDto orderDto);
        IResult Delete(Guid id);
    }
}
