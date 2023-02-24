using AutoMapper;
using Logistics.Business.Constants;
using Logistics.Core.Entities.Exceptions;
using Logistics.Core.Utilities.Results;
using Logistics.DataAccess.Abstract;
using Logistics.Entity;

namespace Logistics.Business
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public IDataResult<IQueryable<OrderVm>> GetListQueryableOdata()
        {
            var entityList = _orderRepository.GetAll().OrderByDescending(x => x.AddDate);
            var orderVmList = _mapper.ProjectTo<OrderVm>(entityList);
            return new SuccessDataResult<IQueryable<OrderVm>>(orderVmList);
        }
        public IDataResult<OrderVm> GetById(Guid id)
        {
            var entity = _orderRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var orderVm = _mapper.Map<OrderVm>(entity);
            return new SuccessDataResult<OrderVm>(orderVm);
        }
        public IDataResult<int> Post(OrderDto orderDto)
        {
            var addEntity = _mapper.Map<Order>(orderDto);
            addEntity.Status = (byte)EnumOrderStatus.OrderReceived;
            _orderRepository.Add(addEntity);
            return new SuccessDataResult<int>(orderDto.OrderNo);
        }

        public IDataResult<OrderDto> Update(OrderDto orderDto)
        {
            var order = _orderRepository.GetById(orderDto.Id);
            if (order == null) { throw new NotFoundException(orderDto.Id); }
            order = _mapper.Map(orderDto, order);
            _orderRepository.Update(order);
            return new SuccessDataResult<OrderDto>(orderDto);
        }
        public IResult Delete(Guid id)
        {
            var entity = _orderRepository.GetById(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.OrderNotFound);
            }
            _orderRepository.Delete(entity);
            return new SuccessResult(Messages.OrderDeleted);
        }
    }
}
