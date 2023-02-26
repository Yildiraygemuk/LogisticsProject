using Logistics.Core.Utilities.Results;
using Logistics.Entity;

namespace Logistics.Business
{
    public interface IProductService
    {
        IDataResult<IQueryable<ProductVm>> GetListQueryable();
        IDataResult<ProductVm> GetById(Guid id);
        IDataResult<ProductDto> Post(ProductDto productDto);
        IDataResult<ProductDto> Update(ProductDto productDto);
        IResult Delete(Guid id);
    }
}
