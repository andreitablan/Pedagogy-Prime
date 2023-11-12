using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace PedagogyPrime.API
{
	using MediatR;
	using Microsoft.OpenApi.Models;
	using Security;

	public static class APIServices
	{
		public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(
				option =>
				{
					option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
					{
						In = ParameterLocation.Header,
						Description = "Please enter a valid token",
						Name = "Authorization",
						Type = SecuritySchemeType.Http,
						BearerFormat = "JWT",
						Scheme = "Bearer"
					});

					option.AddSecurityDefinition("Basic", new OpenApiSecurityScheme());

					option.AddSecurityRequirement(new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[]{}
						}
					});
				});

			services.AddHttpContextAccessor();
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatrRequestContextBehaviour<,>));

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


			services.AddAPIAuthentication(configuration);

			services.AddAPIAuthorization();

			//TODO: add "AllowedCorsHosts": "http://localhost:port" to applicationsettings.Development when connecting with frontend
			string[] allowedCorsHosts = null ?? new[] { "*" };

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					policyBuilder =>
					{
						policyBuilder.WithOrigins(allowedCorsHosts);
						policyBuilder.AllowAnyHeader();
						policyBuilder.AllowAnyMethod();
					});
			});

			return services;
		}
	}
}