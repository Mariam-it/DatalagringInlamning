using Datalagring.Contexts;
using Datalagring.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Datalagring.Repositories;

public class UserRepository(DataContext context) : Repo<DataContext, UserEntity>(context)
{
    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        try
        {
            var existingEntity = await _context.Users.Include(i => i.Profile).Include(i => i.Role).Include(i => i.Address).ToListAsync();
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public override async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Users.Include(i => i.Profile).Include(i => i.Role).Include(i => i.Address).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public override async Task<UserEntity> Updatesync(Expression<Func<UserEntity, bool>> expression, UserEntity entity)
    {
        try
        {
            var existingEntity = await _context.Users.Include(i => i.Profile).Include(i => i.Address).Include(I => I.Role).FirstOrDefaultAsync(expression);
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

}
