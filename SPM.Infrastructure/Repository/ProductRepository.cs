using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPM.Infrastructure.Context;
using SPM.Infrastructure.Entities;
using SPM.Infrastructure.Exception;

namespace SPM.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<IProductRepository> _logger;

    // ReSharper disable once ContextualLoggerProblem
    public ProductRepository(MainContext mainContext, ILogger<IProductRepository>? logger = null)
    {
        _mainContext = mainContext ?? throw new ArgumentNullException(nameof(mainContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task<List<Product>> Get()
    {
        var products = await (_mainContext.Product ?? throw new InvalidOperationException("DB Context not found"))
            .ToListAsync();
        
        return products;
    }

    public async Task<Product> GetById(Guid id)
    {
        var product =
            await (_mainContext.Product ?? throw new InvalidOperationException("Db Context not found"))
                .SingleOrDefaultAsync(product => product.Id == id);

        if (product != null)
        {
            return product;
        }

        _logger.LogError("Invalid GetById operation - can't find id = {ProductId:guid}", id);
        throw new ProductNotFoundException("No product found");
    }

    public async Task Add(Product entity)
    {
        var productExist = await (_mainContext.Product ?? throw new InvalidOperationException("DB Context not found"))
            .AnyAsync(product => product.Id == entity.Id);

        try
        {
            if (!productExist)
            {
                await _mainContext.AddAsync(entity);
                await _mainContext.SaveChangesAsync();
            }
        }
        catch
        {
            throw new ProductNotFoundException("Product already exist in database");
        }
    }

    public async Task Update(Product entity)
    {
        var productToUpdate = await (_mainContext.Product ?? throw new InvalidOperationException())
            .SingleOrDefaultAsync(product => product.Id == entity.Id);

        try
        {
            if (productToUpdate != null)
            {
                productToUpdate.Id = entity.Id;
                productToUpdate.Name = entity.Name;
                productToUpdate.Price = entity.Price;
                productToUpdate.CopiesSold = entity.CopiesSold;

                await _mainContext.SaveChangesAsync();
            }
        }
        catch
        {
            _logger.LogError("Invalid GetById operation - can't find id = {ProductId:guid}", entity.Id);
            throw new ProductNotFoundException("Product doesn't exist in database");
        }
    }

    public async Task DeleteById(Guid id)
    {
        var productToDelete = await (_mainContext.Product ?? throw new InvalidOperationException())
            .SingleOrDefaultAsync(product => product.Id == id);

        try
        {
            if (productToDelete != null)
            {
                _mainContext.Product.Remove(productToDelete);
                await _mainContext.SaveChangesAsync();
            }
        }
        catch
        {
            _logger.LogError("Invalid GetById operation - can't find id = {ProductId}", id);
            throw new ProductNotFoundException("Product doesn't exist in database");
        }
    }
}