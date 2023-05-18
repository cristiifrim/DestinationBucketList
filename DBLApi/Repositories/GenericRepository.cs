using DBLApi.Data;
using DBLApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBLApi.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return await SaveChanges();
    }

    public async Task<bool> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await SaveChanges();
    }

    public virtual async Task<IEnumerable<T>> GetAll(int page, int pageSize)
    {
        return await _context.Set<T>()
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<int> NumberOfPages(int pageSize = 30)
    {
        int total = await _context.Set<T>().CountAsync();
        int totalPages = total / pageSize;
        
        if (total % pageSize > 0)
            totalPages++;

        return totalPages;
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return await SaveChanges();
    }
}