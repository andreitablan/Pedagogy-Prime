namespace PedagogyPrime.Infrastructure.Commands.Authentication
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using MediatR;
	using Microsoft.Extensions.Configuration;
	using Microsoft.IdentityModel.Tokens;
	using Models.User;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.Common.monitor;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;

	public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<LoginResult>>
	{
		private readonly IUserRepository userRepository;
		private readonly IConfiguration configuration;
        private readonly SafetyMonitor safetyMonitor;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = userRepository;
			this.configuration = configuration;
			this.safetyMonitor = new SafetyMonitor();
			//subscribe to events
            safetyMonitor.SafetyPropertyViolated += HandleSafetyViolation;
            safetyMonitor.SafetyPropertyValidated += HandleSafetyValidation;
        }
        [HandlerAspect]
        public async Task<BaseResponse<LoginResult>> Handle(
			LoginCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var usersWithSameCredentials = await userRepository.GetByCredentials(request.Username, request.Password);

				if(usersWithSameCredentials.Count == 0)
				{
                    safetyMonitor.OnSafetyPropertyViolated("No user found during login attempt"); //too many attempts? email the user or block the access
                    return BaseResponse<LoginResult>.NotFound("User");
				}

				if(usersWithSameCredentials.Count > 1)
				{
                    safetyMonitor.OnSafetyPropertyViolated("Multiple users found with the same credentials");
                    return BaseResponse<LoginResult>.InternalServerError("There was a problem with your login. Please contact the application administrator.");
				}

				var user = usersWithSameCredentials.First();

				GenerateToken(user, out var tokenHandler, out var token);

				var loginResult = new LoginResult
				{
					UserDetails = GenericMapper<User, UserDetails>.Map(user),
					AccessToken = tokenHandler.WriteToken(token)
				};
                safetyMonitor.OnSafetyPropertyValidated("Login process successfully validated");

                return BaseResponse<LoginResult>.Ok(loginResult);
			}
			catch (Exception ex)
			{
                safetyMonitor.OnSafetyPropertyViolated($"Exception during login: {ex.Message}");
                return BaseResponse<LoginResult>.InternalServerError();
			}
		}

		private void GenerateToken(
			User user, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token
		)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
			};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(configuration.GetSection("Token").Value!));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = credentials
			};

			tokenHandler = new JwtSecurityTokenHandler();

			token = tokenHandler.CreateToken(tokenDescriptor);
		}
        private static void HandleSafetyViolation(object sender, SafetyViolationEventArgs e)
        {
            Console.WriteLine($"HANDLING SAFETY VIOLATION: {e.Message}");
        }

        private static void HandleSafetyValidation(object sender, SafetyValidationEventArgs e)
        {
            Console.WriteLine($"HANDLING SAFETY VALIDATION: {e.Message}");
        }
    }
}