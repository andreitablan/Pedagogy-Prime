using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.Users.Create;
	using Infrastructure.Commands.Users.Delete;
	using Infrastructure.Commands.Users.Update;
	using Infrastructure.Models.Subject;
	using Infrastructure.Models.User;
	using Infrastructure.Queries.Users.GetAll;
	using Infrastructure.Queries.Users.GetById;
	using Infrastructure.Queries.UsersSubjects.GetAllForUser;
	using MediatR;
	using PedagogyPrime.API.AOP;

	public class UsersController : BaseController
	{
		public UsersController(
			IMediator mediator
		) : base(mediator)
		{
		}

		[HttpGet]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<List<UserDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllUsersQuery()));
		}

		[HttpGet("{id}")]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<UserDetails>> GetById(
			Guid id
		)
		{
			return HandleResponse(await _mediator.Send(new GetUserByIdQuery(id)));
		}

		[HttpPost]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<bool>> Create(
			[FromBody] CreateUserCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<UserDetails>> Update(
			Guid id,
			[FromBody] UpdateUserCommand command
		)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<bool>> Delete(
			Guid id
		)
		{
			return HandleResponse(await _mediator.Send(new DeleteUserCommand(id)));
		}

		[HttpGet("{id}/subjects")]
        [TraceApiAspect(nameof(UsersController))]
        public async Task<ActionResult<List<SubjectDetails>>> GetAllSubjectsForUser(
			Guid id
		)
		{
			return HandleResponse(await _mediator.Send(new GetAllSubjectsForUserQuery()));
		}
	}
}