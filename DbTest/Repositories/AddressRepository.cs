using Datalagring.Contexts;
using Datalagring.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Datalagring.Repositories;
public class AddressRepository(DataContext context) : Repo<DataContext, AddressRepository>(context)
{
}
