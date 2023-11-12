namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Core.IRepositories;
	using Microsoft.EntityFrameworkCore;

	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(
			PedagogyPrimeDbContext context
		) : base(context)
		{
		}

		public async Task<List<User>?> GetByCredentials(
			string username,
			string password
		)
		{
			return await _context.Users.Where(x => x.Username == username && x.Password == password).ToListAsync();
		}
	}
}