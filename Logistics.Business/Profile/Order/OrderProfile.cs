using AutoMapper;
using Logistics.Entity;

namespace Logistics.Business
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
