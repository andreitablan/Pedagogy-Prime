namespace PedagogyPrime.Infrastructure
{
	using Authorization;
	using IAuthorization;
	using MediatR;
	using Microsoft.Extensions.DependencyInjection;
	using PedagogyPrime.Infrastructure.Commands.Homeworks.Create;
	using PedagogyPrime.Infrastructure.Common;

	public static class InfrastructureServices
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(CreateHomeworkCommand).Assembly); });

			services.AddScoped<IUserAuthorization, UserAuthorization>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            return services;
		}
	}
}