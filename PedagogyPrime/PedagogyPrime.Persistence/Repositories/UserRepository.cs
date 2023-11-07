namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Core.IRepositories;

	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(
			PedagogyPrimeDbContext context
		) : base(context)
		{
		}
	}
}