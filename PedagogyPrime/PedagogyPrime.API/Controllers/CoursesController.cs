using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedagogyPrime.Infrastructure.Commands.Courses.Create;
using PedagogyPrime.Infrastructure.Commands.Courses.Delete;
using PedagogyPrime.Infrastructure.Commands.Courses.Update;
using PedagogyPrime.Infrastructure.Models.Course;
using PedagogyPrime.Infrastructure.Queries.Courses.GetAll;
using PedagogyPrime.Infrastructure.Queries.Courses.GetById;

namespace PedagogyPrime.API.Controllers
{
	public class CoursesController : BaseController
	{
		public CoursesController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<ActionResult<List<CourseDetails>>> GetAll()
		{
			return HandleResponse(await _mediator.Send(new GetAllCoursesQuery()));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CourseDetails>> GetById(Guid id)
		{
			var query = new GetCourseByIdQuery
			{
				Id = id
			};

			return HandleResponse(await _mediator.Send(query));
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(
			[FromBody] CreateCourseCommand command
		)
		{
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CourseDetails>> Update(Guid id, [FromBody] UpdateCourseCommand command)
		{
			command.Id = id;
			return HandleResponse(await _mediator.Send(command));
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(Guid id)
		{
			var command = new DeleteCourseCommand
			{
				Id = id
			};
			return HandleResponse(await _mediator.Send(command));
		}
	}
}