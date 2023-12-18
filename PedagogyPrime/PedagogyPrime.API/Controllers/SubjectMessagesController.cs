using Microsoft.AspNetCore.Mvc;

namespace PedagogyPrime.API.Controllers
{
	using Infrastructure.Commands.SubjectMessages.Create;
	using Infrastructure.Queries.SubjectMessage.GetMessagesForSubject;
	using MediatR;
	using PedagogyPrime.Infrastructure.Models.Message;

	public class SubjectMessagesController : BaseController
	{
		public SubjectMessagesController(
			IMediator mediator
		) : base(mediator)
		{
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Add(
			CreateSubjectMessageCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpGet("{subjectId}")]
		public async Task<ActionResult<List<MessageDetails>>> GetMessages(
			Guid subjectId
		)
		{
			return HandleResponse(await _mediator.Send(new GetMessagesForSubject(subjectId)));
		}
	}
}