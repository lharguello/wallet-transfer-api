using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WalletTransfer.Api.Core.Interfaces.Repositories;
using WalletTransfer.Api.Infrastructure.Data.EntityFramework;

namespace WalletTransfer.Api.Infrastructure.Data.Repositories;

public class GenericRepository<T>(ApplicationDBContext dbContext) : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDBContext _dbContext = dbContext;

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        catch (MySqlException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Internal error in Entity[{string.Join(",", ex.Entries?.Select(p => p.Entity.GetType().Name)!)}] with state[{ex.Entries?.FirstOrDefault()!.State}]", ex.InnerException ?? new Exception(ex.Message));
        }
        catch (Exception ex)
        {
            throw new Exception("Internal error", ex);
        }
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        try
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        catch (MySqlException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception($"Internal error in Entity[{string.Join(",", ex.Entries?.Select(p => p.Entity.GetType().Name)!)}] with state[{ex.Entries?.FirstOrDefault()!.State}]", ex.InnerException ?? new Exception(ex.Message));
        }
        catch (Exception ex)
        {
            throw new Exception("Internal error", ex);
        }
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
