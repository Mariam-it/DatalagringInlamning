using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class CategoryRepository(ProductCatalogContext context) : Repo<ProductCatalogContext ,Category>(context)
{
}
