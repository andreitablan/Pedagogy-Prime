using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Documents.Create;
using PedagogyPrime.Infrastructure.Commands.Documents.Delete;
using PedagogyPrime.Infrastructure.Commands.Documents.Update;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Models.Homework;
using PedagogyPrime.Infrastructure.Queries.Documents.GetAll;
using PedagogyPrime.Infrastructure.Queries.Documents.GetById;
using PedagogyPrime.Infrastructure.Queries.Homeworks.GetAll;

namespace PedagogyPrime.API.Controllers
{
	public class HomeworksController : BaseController
	{
		public HomeworksController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		public async Task<ActionResult<List<HomeworkDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllHomeworkQuery()));
		}

		/*
		[HttpGet("{id}")]
		public async Task<ActionResult<HomeworkDetails>> GetById(Guid id)
		{
			var query = new GetDocumentByIdQuery
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(query));
		}

		[HttpPost]
		public async Task<ActionResult<bool>> Create(
			[FromBody] CreateDocumentCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<DocumentDetails>> Update(Guid id, [FromBody] UpdateDocumentCommand command)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(Guid id)
		{
			var command = new DeleteDocumentCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}*/
	}
}