namespace PedagogyPrime.Infrastructure.Authorization
{
	using Core.Entities;
	using IAuthorization;
	using Microsoft.EntityFrameworkCore;
	using Persistence.Context;

	public class UserAuthorization : IUserAuthorization
	{
		private readonly PedagogyPrimeDbContext context;

		public UserAuthorization(PedagogyPrimeDbContext context)
		{
			this.context = context;
		}

		public bool IsRequestForItself(
			Guid userId,
			Guid resourceUserId
		)
		{
			return userId.Equals(resourceUserId);
		}

		public async Task<bool> IsAdmin(
			Guid userId
		)
		{
			var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

			if(user == null)
			{
				return false;
			}

			return user.Role == Role.Admin;
		}
	}
}
