using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Subjects.Create;
using PedagogyPrime.Infrastructure.Commands.Subjects.Delete;
using PedagogyPrime.Infrastructure.Commands.Subjects.Update;
using PedagogyPrime.Infrastructure.Models.Subject;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Infrastructure.Queries.Subjects.GetAll;
using PedagogyPrime.Infrastructure.Queries.Subjects.GetById;
using PedagogyPrime.Infrastructure.Queries.UsersSubjects.GetAllUsersForSubject;

namespace PedagogyPrime.API.Controllers
{
	public class SubjectsController : BaseController
	{
		public SubjectsController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		public async Task<ActionResult<List<SubjectDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllSubjectsQuery()));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SubjectInfo>> GetById(Guid id)
		{
			var query = new GetSubjectByIdQuery
			{
				Id = id
			};

			return HandleResponse(await _mediator.Send(query));
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(
			   [FromBody] CreateSubjectCommand command
		   )
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<SubjectDetails>> Update(
			Guid id,
			[FromBody] UpdateSubjectCommand command
		)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(
			Guid id
		)
		{
			var command = new DeleteSubjectCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpGet("{id}/users")]
		public async Task<ActionResult<List<UserDetails>>> GetAllUsers(Guid id)
		{
			var command = new GetAllUsersForSubjectQuery
			{
				SubjectId = id
			};

			return HandleResponse(await _mediator.Send(command));
		}
	}
}