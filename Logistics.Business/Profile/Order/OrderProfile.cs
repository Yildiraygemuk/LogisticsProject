using AutoMapper;
using Logistics.Entity;

namespace Logistics.Business
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>()
                 .ForMember(dest => dest.StatusValue,
                    act => act.MapFrom(src => EnumHelper<EnumOrderStatus>.GetDisplayValue((EnumOrderStatus)src.Status)))
                 .ForMember(dest => dest.UnitQuantitiesValue,
                    act => act.MapFrom(src => EnumHelper<EnumUnitQuantities>.GetDisplayValue((EnumUnitQuantities)src.UnitQuantities)))
                 .ForMember(dest => dest.UnitWeighValue,
                    act => act.MapFrom(src => EnumHelper<EnumUnitWeigh>.GetDisplayValue((EnumUnitWeigh)src.UnitWeigh)));
            CreateMap<OrderVm, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, StatuDto>().ReverseMap();
            CreateMap<OrderDto, Order>()
               .ForPath(dest => dest.Product.Code, act => act.MapFrom(src => src.ProductCode))
               .ForPath(dest => dest.Product.Name, act => act.MapFrom(src => src.ProductName));
        }
    }
}
