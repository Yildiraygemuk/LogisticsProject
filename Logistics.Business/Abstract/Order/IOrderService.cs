using Logistics.Core.Utilities.Results;
using Logistics.Entity;

namespace Logistics.Business
{
    public interface IOrderService
    {
        IDataResult<IQueryable<OrderVm>> GetListQueryableOdata();
        IDataResult<OrderVm> GetById(Guid id);
        IDataResult<IQueryable<string>> Post(List<OrderDto> orderDto);
        IDataResult<StatuDto> Update(StatuDto statuDto);
        IResult Delete(Guid id);
    }
}
