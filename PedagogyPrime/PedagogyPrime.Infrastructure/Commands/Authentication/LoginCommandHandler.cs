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
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;

	public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<LoginResult>>
	{
		private readonly IUserRepository userRepository;
		private readonly IConfiguration configuration;

		public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = userRepository;
			this.configuration = configuration;
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
					return BaseResponse<LoginResult>.NotFound("User");
				}

				if(usersWithSameCredentials.Count > 1)
				{
					return BaseResponse<LoginResult>.InternalServerError("There was a problem with your login. Please contact the application administrator.");
				}

				var user = usersWithSameCredentials.First();

				GenerateToken(user, out var tokenHandler, out var token);

				var loginResult = new LoginResult
				{
					UserDetails = GenericMapper<User, UserDetails>.Map(user),
					AccessToken = tokenHandler.WriteToken(token)
				};

				return BaseResponse<LoginResult>.Ok(loginResult);
			}
			catch
			{
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
	}
}