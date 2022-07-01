using SPM.Infrastructure.Entities;
using SPM.Infrastructure.Repository;

namespace SPM.Core.Services;

public interface IProductService
{
    public Task<List<Product>> Get();

    public Task<Product> GetById(Guid id);

    public Task Add(Product entity);

    public Task Update(Product entity);

    public Task DeleteById(Guid id);
    
}