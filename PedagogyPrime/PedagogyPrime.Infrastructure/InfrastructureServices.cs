namespace PedagogyPrime.Infrastructure
{
	using Authorization;
	using Commands.Users.Create;
	using IAuthorization;
	using Microsoft.Extensions.DependencyInjection;

	public static class InfrastructureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateHomeworkCommand).Assembly); });

			services.AddScoped<IUserAuthorization, UserAuthorization>();
			return services;
		}
	}
}