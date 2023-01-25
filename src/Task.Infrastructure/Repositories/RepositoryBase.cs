using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task.App.DataBaseContext;

namespace Task.App.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly AppDbContext dbContext;

    public RepositoryBase(AppDbContext dbContext)
    {
        dbContext = dbContext;
    }
    public async ValueTask<IQueryable<T>> FindAllAsync(string[] includes = null, bool trackChanges = true)
    {
        DbSet<T> dbSet = this.dbContext
            .Set<T>();
        if (includes is not null)
            foreach (var includeParam in includes)
                dbSet.Include(includeParam);
        if (!trackChanges)
            dbSet.AsTracking(QueryTrackingBehavior.NoTracking);
        return dbSet;
    }

    public async ValueTask<T> FindByExpressionAsync(Expression<Func<T, bool>> expression, string[] includes = null,
        bool trackChanges = true)
    {
        return await (await FindAllAsync(includes, trackChanges))
            .FirstOrDefaultAsync(expression);
    }

    public async ValueTask<T> CreateAsync(T entity)
    {
        return (
                await this.dbContext
                .Set<T>()
                .AddAsync(entity))
            .Entity;
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        return this.dbContext
            .Set<T>()
            .Update(entity)
            .Entity;
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        return this.dbContext
            .Set<T>()
            .Remove(entity)
            .Entity;
    }
}