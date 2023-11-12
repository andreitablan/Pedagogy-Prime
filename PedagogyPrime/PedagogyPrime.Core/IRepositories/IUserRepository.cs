namespace PedagogyPrime.Core.IRepositories
{
	using Entities;

	public interface IUserRepository : IBaseRepository<User>
	{
		Task<List<User>?> GetByCredentials(
			string username,
			string password
		);
	}
}