
using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class SizeRepository(ProductCatalogContext context) : Repo<ProductCatalogContext,Size>(context)
{
}
