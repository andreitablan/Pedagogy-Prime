using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.Authentication;
	using Infrastructure.Models.User;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using PedagogyPrime.API.AOP;

	[AllowAnonymous]
	public class AuthenticationController : BaseController
	{
		public AuthenticationController(
			IMediator mediator
		) : base(mediator)
		{
		}

        [HttpPost("login")]
        [TraceApiAspect(nameof(AuthenticationController))]
        public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command)
		{
			return HandleResponse(await _mediator.Send(command));
		}
	}
}