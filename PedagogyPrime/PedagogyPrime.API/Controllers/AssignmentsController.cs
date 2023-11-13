﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Assignments.Create;
using PedagogyPrime.Infrastructure.Commands.Assignments.Delete;
using PedagogyPrime.Infrastructure.Commands.Assignments.Update;
using PedagogyPrime.Infrastructure.Commands.Documents.Create;
using PedagogyPrime.Infrastructure.Commands.Documents.Delete;
using PedagogyPrime.Infrastructure.Commands.Documents.Update;
using PedagogyPrime.Infrastructure.Commands.Homeworks.Create;
using PedagogyPrime.Infrastructure.Commands.Users.Delete;
using PedagogyPrime.Infrastructure.Commands.Users.Update;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Models.Homework;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Infrastructure.Queries.Assignments.GetAll;
using PedagogyPrime.Infrastructure.Queries.Assignments.GetById;
using PedagogyPrime.Infrastructure.Queries.Documents.GetAll;
using PedagogyPrime.Infrastructure.Queries.Documents.GetById;
using PedagogyPrime.Infrastructure.Queries.Homeworks.GetAll;

namespace PedagogyPrime.API.Controllers
{
	public class AssignmentsController : BaseController
	{
		public AssignmentsController(IMediator mediator)
			: base(mediator)
		{
		}

		[HttpGet]
		public async Task<ActionResult<List<AssignmentDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllAssignmentsQuerry()));
		}

		
		[HttpGet("{id}")]
		public async Task<ActionResult<AssignmentDetails>> GetById(Guid id)
		{
			var query = new GetAssignmentByIdQuery(id);
			return HandleResponse(await _mediator.Send(query));
		}

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateAssignmentCommand command
        )
        {
            return HandleResponse(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentDetails>> Update(
            Guid id,
            [FromBody] UpdateAssignmentCommand command
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
            return HandleResponse(await _mediator.Send(new DeleteAssignmentCommand(id)));
        }
    }
}