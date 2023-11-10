using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Courses.Delete;
using PedagogyPrime.Infrastructure.Commands.Courses.Update;
using PedagogyPrime.Infrastructure.Commands.Documents.Create;
using PedagogyPrime.Infrastructure.Commands.Documents.Delete;
using PedagogyPrime.Infrastructure.Commands.Documents.Update;
using PedagogyPrime.Infrastructure.Commands.Users.Create;
using PedagogyPrime.Infrastructure.Models.Course;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Infrastructure.Queries.Courses.GetById;
using PedagogyPrime.Infrastructure.Queries.Documents.GetAll;
using PedagogyPrime.Infrastructure.Queries.Documents.GetById;
using PedagogyPrime.Infrastructure.Queries.Users.GetAll;

namespace PedagogyPrime.API.Controllers
{
    public class DocumentsController : BaseController
    {
        public DocumentsController(IMediator mediator)
            : base(mediator) 
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentDetails>>> GetAll()
        {
            return HandleResponse(await _mediator.Send(new GetAllDocumentsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDetails>> GetById(Guid id)
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
            var command = new DeleteDocumentCommand { UserId = id };
            return HandleResponse(await _mediator.Send(command));
        }
    }
}
