using SPM.Infrastructure.Entities;

namespace SPM.Infrastructure.Repository;

public interface IRepository<T>
{
    Task<List<Product>> Get();
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task DeleteById(Guid id);
}