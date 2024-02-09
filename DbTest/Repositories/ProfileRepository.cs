using Datalagring.Contexts;
using Datalagring.Entities;

namespace Datalagring.Repositories;

public class ProfileRepository(DataContext context) : Repo<DataContext, ProfileEntity>(context)
{
}

