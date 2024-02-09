using Datalagring.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datalagring.Repositories;

public abstract class Repo<TContext, TEntity> where TContext : DbContext where TEntity : class
{
    protected readonly TContext _context;

    protected Repo(TContext context)
    {
        _context = context;
    }


    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch { }
        return null!;
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Set<TEntity>().ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }

    public virtual async Task<TEntity> Updatesync(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                _context.Set<TEntity>().Remove(existingEntity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch { }
        return false;
    }

    public virtual async Task<bool> ExistingAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existing = await _context.Set<TEntity>().AnyAsync(expression);
            return existing;
        }
        catch { }
        return false;
    }
}
//public abstract class Repo<TEntity> where TEntity : class
//{
//    private readonly DataContext _context;

//    protected Repo(DataContext context)
//    {
//        _context = context;
//    }


//    public virtual async Task<TEntity> CreateAsync(TEntity entity)
//    {
//        try
//        {
//            _context.Set<TEntity>().Add(entity);
//            await _context.SaveChangesAsync();

//            return entity;
//        }
//        catch { }
//        return null!;
//    }

//    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
//    {
//        try
//        {
//            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
//            if (existingEntity != null)
//            {
//                return existingEntity;
//            }
//        }
//        catch { }
//        return null!;
//    }

//    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
//    {
//        try
//        {
//            var existingEntities = await _context.Set<TEntity>().ToListAsync();
//            if (existingEntities != null)
//            {
//                return existingEntities;
//            }
//        }
//        catch { }
//        return null!;
//    }

//    public virtual async Task<TEntity> Updatesync(Expression<Func<TEntity, bool>> expression, TEntity entity)
//    {
//        try
//        {
//            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
//            if (existingEntity != null)
//            {
//                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
//                await _context.SaveChangesAsync();
//                return existingEntity;
//            }
//        }
//        catch { }
//        return null!;
//    }

//    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
//    {
//        try
//        {
//            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
//            if (existingEntity != null)
//            {
//                _context.Set<TEntity>().Remove(existingEntity);
//                await _context.SaveChangesAsync();
//                return true;
//            }
//        }
//        catch { }
//        return false;
//    }

//    public virtual async Task<bool> ExistingAsync(Expression<Func<TEntity, bool>> expression)
//    {
//        try
//        {
//            var existing = await _context.Set<TEntity>().AnyAsync(expression);
//            return existing;
//        }
//        catch { }
//        return false;
//    }
//}
