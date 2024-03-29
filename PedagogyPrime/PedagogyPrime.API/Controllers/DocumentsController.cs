﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.API.AOP;
using PedagogyPrime.Infrastructure.Commands.Documents.Create;
using PedagogyPrime.Infrastructure.Commands.Documents.Delete;
using PedagogyPrime.Infrastructure.Commands.Documents.Update;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Queries.Documents.GetAll;
using PedagogyPrime.Infrastructure.Queries.Documents.GetById;

namespace PedagogyPrime.API.Controllers
{
	public class DocumentsController : BaseController
	{
		public DocumentsController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
        [TraceApiAspect(nameof(DocumentsController))]
        public async Task<ActionResult<List<DocumentDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllDocumentsQuery()));
		}

		[HttpGet("{id}")]
        [TraceApiAspect(nameof(DocumentsController))]
        public async Task<ActionResult<DocumentDetails>> GetById(Guid id)
		{
			var query = new GetDocumentByIdQuery
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(query));
		}

		[HttpPost]
        [TraceApiAspect(nameof(DocumentsController))]
        public async Task<ActionResult<bool>> Create(
			[FromBody] CreateDocumentCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
        [TraceApiAspect(nameof(DocumentsController))]
        public async Task<ActionResult<DocumentDetails>> Update(Guid id, [FromBody] UpdateDocumentCommand command)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
        [TraceApiAspect(nameof(DocumentsController))]
        public async Task<ActionResult<bool>> Delete(Guid id)
		{
			var command = new DeleteDocumentCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}
	}
}