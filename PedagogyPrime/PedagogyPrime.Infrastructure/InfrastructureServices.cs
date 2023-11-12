namespace PedagogyPrime.Infrastructure
{
	using Commands.Users.Create;
	using Microsoft.Extensions.DependencyInjection;

	public static class InfrastructureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly); });
			return services;
		}
	}
}