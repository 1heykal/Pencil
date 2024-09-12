using System.Linq.Expressions;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : class 
{
     Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    
     Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> wherePredicate,
        CancellationToken cancellationToken = default);

     Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

     Task<T?> GetAsync(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken = default);
     
     Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

     Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

     Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

     Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}