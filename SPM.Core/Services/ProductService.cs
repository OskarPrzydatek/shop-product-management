using SPM.Infrastructure.Entities;
using SPM.Infrastructure.Repository;

namespace SPM.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<List<Product>> Get()
    {
        return await _productRepository.Get();
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task Add(Product entity)
    {
        await _productRepository.Add(entity);
    }

    public async Task Update(Product entity)
    {
        await _productRepository.Update(entity);
    }

    public async Task DeleteById(Guid id)
    {
        await _productRepository.DeleteById(id);
    }
}