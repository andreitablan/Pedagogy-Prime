using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace PedagogyPrime.API
{
	public static class APIServices
	{
		public static IServiceCollection AddAPIServices(this IServiceCollection services)
		{
			services.AddApiVersioning(o =>
			{
				o.DefaultApiVersion = new ApiVersion(1, 0);
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.ReportApiVersions = true;
				o.ApiVersionReader = ApiVersionReader.Combine(
					new QueryStringApiVersionReader("api-version"),
					new HeaderApiVersionReader("X-Version"),
					new MediaTypeApiVersionReader("ver"));
			});

			return services;
		}
	}
}