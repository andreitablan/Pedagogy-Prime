using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.Users.Create;
	using Infrastructure.Models.User;
	using Infrastructure.Queries.Users.GetAll;
	using MediatR;

	[ApiVersion("1.0")]
	public class UsersController : BaseController
	{
		public UsersController(
			IMediator mediator
		) : base(mediator)
		{
		}

		[HttpPost]
		public async Task<ActionResult<bool>> Create(
			[FromBody] CreateUserCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpGet]
		public async Task<ActionResult<List<UserDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllUsersQuery()));
		}
	}
}