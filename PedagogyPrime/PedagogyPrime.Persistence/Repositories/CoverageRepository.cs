namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Core.IRepositories;

	public class CoverageRepository : BaseRepository<Coverage>, ICoverageRepository
	{
		protected CoverageRepository(
			PedagogyPrimeDbContext context
		) : base(context)
		{
		}
	}
}