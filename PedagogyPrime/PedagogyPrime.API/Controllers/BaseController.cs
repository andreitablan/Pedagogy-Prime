namespace PedagogyPrime.API.Controllers
{
	using Core.Common;
	using MediatR;
	using Microsoft.AspNetCore.Mvc;
	using CustomStatusCodes = Core.Common.StatusCodes;
	using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

	[ApiController, Route("api/v{version:apiVersion}/[controller]")]
	public class BaseController : ControllerBase
	{
		/// <summary>
		/// </summary>
		protected readonly IMediator _mediator;

		/// <summary>
		///     constructor
		/// </summary>
		/// <param name="mediator"></param>
		public BaseController(IMediator mediator)
		{
			_mediator = mediator;
		}

		/// <summary>
		///     Handles the response from the mediatr
		/// </summary>
		/// <param name="response"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected ActionResult<T> HandleResponse<T>(BaseResponse<T> response)
		{
			return response.StatusCode switch
			{
				CustomStatusCodes.Ok => Ok(response),
				CustomStatusCodes.NotFound => NotFound(),
				CustomStatusCodes.BadRequest => BadRequest(response),
				CustomStatusCodes.Forbidden => Forbid(),
				CustomStatusCodes.Created => StatusCode(StatusCodes.Status201Created),
				CustomStatusCodes.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError)
			};
		}
	}
}