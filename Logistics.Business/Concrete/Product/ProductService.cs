using AutoMapper;
using AutoMapper.Features;
using Logistics.Business.Constants;
using Logistics.Core.Entities.Exceptions;
using Logistics.Core.Utilities.Results;
using Logistics.DataAccess;
using Logistics.Entity;

namespace Logistics.Business
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IDataResult<IQueryable<ProductVm>> GetListQueryable()
        {
            var entityList = _productRepository.GetAll().OrderByDescending(x => x.AddDate);
            var productVmList = _mapper.ProjectTo<ProductVm>(entityList);
            return new SuccessDataResult<IQueryable<ProductVm>>(productVmList);
        }
        public IDataResult<ProductVm> GetById(Guid id)
        {
            var entity = _productRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var productVm = _mapper.Map<ProductVm>(entity);
            return new SuccessDataResult<ProductVm>(productVm);
        }
        public IDataResult<ProductDto> Post(ProductDto productDto)
        {
            var addEntity = _mapper.Map<Product>(productDto);
            var result = _productRepository.Add(addEntity);
            productDto.Id = result.Data.Id;
            return new SuccessDataResult<ProductDto>(productDto);
        }
        public IDataResult<ProductDto> Update(ProductDto productDto)
        {
            var product = _productRepository.GetById(productDto.Id);
            if(product == null) { throw new NotFoundException(productDto.Id); }
            product = _mapper.Map(productDto, product);
            _productRepository.Update(product);
            return new SuccessDataResult<ProductDto>(productDto);
        }
        public IResult Delete(Guid id)
        {
            var entity = _productRepository.GetById(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            _productRepository.Delete(entity);
            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
