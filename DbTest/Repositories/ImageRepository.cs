using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class ImageRepository(ProductCatalogContext context) : Repo<ProductCatalogContext,Image>(context)
{
}
