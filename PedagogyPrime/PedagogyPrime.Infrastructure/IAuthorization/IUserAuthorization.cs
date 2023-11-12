namespace PedagogyPrime.Infrastructure.IAuthorization
{
	public interface IUserAuthorization
	{
		bool IsRequestForItself(
			Guid userId,
			Guid resourceUserId
		);

		Task<bool> IsAdmin(
			Guid userId
		);
	}
}
