using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class RoleRepository(DataContext context) : Repo<DataContext, RoleEntity>(context)
{

}
