namespace PedagogyPrime.Infrastructure
{
	using Commands.Users.Create;
	using Microsoft.Extensions.DependencyInjection;
	using PedagogyPrime.Infrastructure.Commands.Courses.Create;
	using PedagogyPrime.Infrastructure.Commands.Documents.Create;

	public static class InfrastructureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly); });
			return services;
		}
	}
}