using AutoMapper;
using Logistics.Business.Constants;
using Logistics.Core.Entities.Exceptions;
using Logistics.Core.Utilities.Results;
using Logistics.DataAccess.Abstract;
using Logistics.Entity;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Text;

namespace Logistics.Business
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductService productService, IRabbitMQService rabbitMQService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _rabbitMQService = rabbitMQService;
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
        public IDataResult<IQueryable<string>> Post(List<OrderDto> orderDto)
        {
            List<Order> orderList = new List<Order>();
            List<OrderDto> errorOrders = new List<OrderDto>();

            foreach (var order in orderDto)
            {
                var orderNoExists = _orderRepository.GetAll()
                    .FirstOrDefault(x => x.OrderNo == order.OrderNo)?.OrderNo != null;

                if (orderNoExists)
                    errorOrders.Add(order);

                var product = _productService.GetListQueryable()
                    .Data.Where(x => x.Code == order.ProductCode).FirstOrDefault();

                if (product == null)
                {
                    var newProduct = new ProductDto()
                    {
                        Code = order.ProductCode,
                        Name = order.ProductName
                    };

                    var addedProduct = _productService.Post(newProduct);
                    order.ProductId = addedProduct.Data.Id;
                }
                else
                {
                    order.ProductId = product.Id;
                }

                order.Status = (byte)EnumOrderStatus.OrderReceived;
                var newOrder = _mapper.Map<Order>(order);
                orderList.Add(newOrder);
            }
            if (errorOrders.Any())
            {
                var errorOrderNos = orderDto.Select(x => x.OrderNo);
                var errorMessage = $"{string.Join(", ", errorOrderNos)} sipariş numaraları sistemde kayıtlı olduğu için eklenemedi.";

                if (orderList.Any())
                    return new ErrorDataResult<IQueryable<string>>(errorOrderNos.AsQueryable(), errorMessage);

                _orderRepository.AddRange(orderList);
                return new SuccessDataResult<IQueryable<string>>(errorOrderNos.AsQueryable(), $"İşlem başarılı fakat {errorMessage}");
            }
            _orderRepository.AddRange(orderList);
            return new SuccessDataResult<IQueryable<string>>(orderDto.Select(x => x.OrderNo).AsQueryable());
        }
        public IDataResult<StatuDto> Update(StatuDto statuDto)
        {
            var order = _orderRepository.GetAll().FirstOrDefault(x => x.OrderNo == statuDto.OrderNo);
            if (order == null) { throw new NotFoundException(statuDto.OrderNo); }
            order = _mapper.Map(statuDto, order);
            var result = _orderRepository.Update(order);
            if (result.Success)
                _rabbitMQService.SendToQueue(order);
            return new SuccessDataResult<StatuDto>(statuDto);
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
