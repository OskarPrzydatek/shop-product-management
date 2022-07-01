using Microsoft.AspNetCore.Mvc;
using SPM.Core.Services;
using SPM.Infrastructure.Entities;

namespace SPM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("/products")]
    public async Task<List<Product>> GetProductsList()
    {
        return await _productService.Get();
    }

    [HttpGet("/product/{id:guid}")]
    public async Task<Product> GetProductById(Guid id)
    {
        return await _productService.GetById(id);
    }

    [HttpPost("/add-product")]
    public async Task AddNewProduct(Product product)
    {
        await _productService.Add(product);
    }

    [HttpPut("/update-product")]
    public async Task UpdateProduct(Product product)
    {
        await _productService.Update(product);
    }

    [HttpDelete("/delete-product/{id:guid}")]
    public async Task DeleteProduct(Guid id)
    {
        await _productService.DeleteById(id);
    }
}