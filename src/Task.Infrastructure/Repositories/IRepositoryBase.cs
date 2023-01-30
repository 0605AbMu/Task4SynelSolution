using System.Linq.Expressions;

namespace Task.App.Repositories;

public interface IRepositoryBase<T> where T: class
{
    ValueTask<IQueryable<T>> FindAllAsync(string[] includes, bool trackChanges);
    ValueTask<T> FindByExpressionAsync(Expression<Func<T, bool>> expression, string[] includes, bool trackChanges);
    ValueTask<T> CreateAsync(T entity);
    ValueTask CreateRangeAsync(IQueryable<T> entities);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
    ValueTask SaveChangesAsync();
}