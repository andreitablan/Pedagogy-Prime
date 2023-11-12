using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.Authentication;
	using Infrastructure.Models.User;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;

	[AllowAnonymous]
	public class AuthenticationController : BaseController
	{
		public AuthenticationController(
			IMediator mediator
		) : base(mediator)
		{
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command)
		{
			return HandleResponse(await _mediator.Send(command));
		}
	}
}