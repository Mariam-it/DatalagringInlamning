using Datalagring.Contexts;
using Datalagring.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Datalagring.Repositories;
public class ProductRepository(ProductCatalogContext context) : Repo<ProductCatalogContext, Product>(context)
{

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var existingEntity = await _context.Products.Include(i => i.Image).Include(i => i.Brand).Include(i => i.Category).Include(i => i.Color).Include(i => i.Size).ToListAsync();
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public override async Task<Product> GetAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Products.Include(i => i.Image).Include(i => i.Brand).Include(i => i.Category).Include(i => i.Color).Include(i => i.Size).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }
}
