namespace PedagogyPrime.Infrastructure
{
	using Authorization;
	using IAuthorization;
	using Microsoft.Extensions.DependencyInjection;
	using PedagogyPrime.Infrastructure.Commands.Homeworks.Create;

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