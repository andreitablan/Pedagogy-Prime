using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Assignments.Create;
using PedagogyPrime.Infrastructure.Commands.Assignments.Delete;
using PedagogyPrime.Infrastructure.Commands.Assignments.Update;
using PedagogyPrime.Infrastructure.Commands.Documents.Create;
using PedagogyPrime.Infrastructure.Commands.Documents.Delete;
using PedagogyPrime.Infrastructure.Commands.Documents.Update;
using PedagogyPrime.Infrastructure.Commands.Homeworks.Create;
using PedagogyPrime.Infrastructure.Commands.Homeworks.Delete;
using PedagogyPrime.Infrastructure.Commands.Homeworks.Update;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Models.Homework;
using PedagogyPrime.Infrastructure.Queries.Documents.GetAll;
using PedagogyPrime.Infrastructure.Queries.Documents.GetById;
using PedagogyPrime.Infrastructure.Queries.Homeworks.GetAll;
using PedagogyPrime.Infrastructure.Queries.Homeworks.GetById;

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

	
		[HttpGet("{id}")]
		public async Task<ActionResult<HomeworkDetails>> GetById(Guid id)
		{
			var query = new GetHomeworkByIdQuery(id);
			
			return HandleResponse(await _mediator.Send(query));
		}

        [HttpPost]
        public async Task<ActionResult<bool>> Create(
               [FromBody] CreateHomeworkCommand command
           )
        {
            return HandleResponse(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeworkDetails>> Update(
            Guid id,
            [FromBody] UpdateHomeworkCommand command
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
            return HandleResponse(await _mediator.Send(new DeleteHomeworkCommand(id)));
        }
    }
}