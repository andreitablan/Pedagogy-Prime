using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.API.AOP;
using PedagogyPrime.Infrastructure.Commands.SubjectForums.Create;
using PedagogyPrime.Infrastructure.Commands.SubjectForums.Delete;
using PedagogyPrime.Infrastructure.Commands.SubjectForums.Update;
using PedagogyPrime.Infrastructure.Models.SubjectForum;
using PedagogyPrime.Infrastructure.Queries.SubjectForums.GetAll;
using PedagogyPrime.Infrastructure.Queries.SubjectForums.GetById;

namespace PedagogyPrime.API.Controllers
{
	public class SubjectForumController : BaseController
	{
		public SubjectForumController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		[TraceApiAspect(nameof(SubjectForumController))]
		public async Task<ActionResult<List<SubjectForumDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllSubjectForumQuery()));
		}

		[HttpGet("{id}")]
		[TraceApiAspect(nameof(SubjectForumController))]
		public async Task<ActionResult<SubjectForumDetails>> GetById(Guid id)
		{
			var query = new GetSubjectForumByIdQuery
			{
				Id = id
			};

			return HandleResponse(await _mediator.Send(query));
		}

		[HttpPost]
		[TraceApiAspect(nameof(SubjectForumController))]
		public async Task<ActionResult<Guid>> Create(
			   [FromBody] CreateSubjectForumCommand command
		   )
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		[TraceApiAspect(nameof(SubjectForumController))]
		public async Task<ActionResult<SubjectForumDetails>> Update(
			Guid id,
			[FromBody] UpdateSubjectForumCommand command
		)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		[TraceApiAspect(nameof(SubjectForumController))]
		public async Task<ActionResult<bool>> Delete(
			Guid id
		)
		{
			var command = new DeleteSubjectForumCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}
	}
}