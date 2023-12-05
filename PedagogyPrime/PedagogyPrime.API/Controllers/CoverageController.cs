using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Coverage.Create;
using PedagogyPrime.Infrastructure.Commands.Coverage.Delete;
using PedagogyPrime.Infrastructure.Commands.Coverage.Update;
using PedagogyPrime.Infrastructure.Models.Coverage;

namespace PedagogyPrime.API.Controllers
{
	public class CoverageController : BaseController
	{
		public CoverageController(IMediator mediator) : base(mediator)
		{
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(
			[FromBody] CreateCoverageCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CoverageDetails>> Update(Guid id, [FromBody] UpdateCoverageCommand command)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(Guid id)
		{
			var command = new DeleteCoverageCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}
	}
}