using AutoMapper;
using Logistics.Entity;

namespace Logistics.Business
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, StatuDto>().ReverseMap();
            CreateMap<OrderDto, Order>()
               .ForPath(dest => dest.Product.Code, act => act.MapFrom(src => src.ProductCode))
               .ForPath(dest => dest.Product.Name, act => act.MapFrom(src => src.ProductName));
        }
    }
}
