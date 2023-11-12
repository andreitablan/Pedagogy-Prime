using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PedagogyPrime.API.Security
{
	internal static class AuthenticationConfiguraiton
	{
		public static IServiceCollection AddAPIAuthentication(
			this IServiceCollection services,
			IConfiguration configuration
			)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey =
							new SymmetricSecurityKey(
								Encoding.UTF8.GetBytes(configuration.GetSection("Token").Value!)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			return services;
		}
	}
}
