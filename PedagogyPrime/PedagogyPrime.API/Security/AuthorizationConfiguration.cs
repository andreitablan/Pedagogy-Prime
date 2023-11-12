namespace PedagogyPrime.API.Security
{
	internal static class AuthorizationConfiguration
	{
		public static IServiceCollection AddAPIAuthorization(
			this IServiceCollection services
		)
		{
			services.AddAuthorization();
			return services;
		}
	}
}
