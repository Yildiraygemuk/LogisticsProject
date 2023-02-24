using AutoMapper;
using Logistics.Entity;

namespace Logistics.Business
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVm>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
