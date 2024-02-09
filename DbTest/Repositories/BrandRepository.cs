using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class BrandRepository(ProductCatalogContext context) : Repo<ProductCatalogContext ,Brand>(context)
{
}