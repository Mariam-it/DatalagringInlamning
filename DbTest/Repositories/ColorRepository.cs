using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;


public class ColorRepository(ProductCatalogContext context) : Repo<ProductCatalogContext,Color>(context)
{
}
