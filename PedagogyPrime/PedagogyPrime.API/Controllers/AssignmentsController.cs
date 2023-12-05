using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.API.AOP;
using PedagogyPrime.Infrastructure.Commands.Assignments.Create;
using PedagogyPrime.Infrastructure.Commands.Assignments.Delete;
using PedagogyPrime.Infrastructure.Commands.Assignments.Update;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Queries.Assignments.GetAll;
using PedagogyPrime.Infrastructure.Queries.Assignments.GetById;

namespace PedagogyPrime.API.Controllers
{
    public class AssignmentsController : BaseController
	{
		public AssignmentsController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
        [TraceApiAspect(nameof(AssignmentsController))]
        public async Task<ActionResult<List<AssignmentDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllAssignmentsQuerry()));
		}

		
		[HttpGet("{id}")]
        [TraceApiAspect(nameof(AssignmentsController))]
        public async Task<ActionResult<AssignmentDetails>> GetById(Guid id)
		{
			var query = new GetAssignmentByIdQuery(id);
			return HandleResponse(await _mediator.Send(query));
		}

        [HttpPost]
        [TraceApiAspect(nameof(AssignmentsController))]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateAssignmentCommand command
        )
        {
            return HandleResponse(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        [TraceApiAspect(nameof(AssignmentsController))]
        public async Task<ActionResult<AssignmentDetails>> Update(
            Guid id,
            [FromBody] UpdateAssignmentCommand command
        )
        {
            command.Id = id;
            return HandleResponse(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [TraceApiAspect(nameof(AssignmentsController))]
        public async Task<ActionResult<bool>> Delete(
            Guid id
        )
        {
            return HandleResponse(await _mediator.Send(new DeleteAssignmentCommand(id)));
        }
    }
}