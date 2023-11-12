namespace PedagogyPrime.API.Controllers
{
	using Core.Common;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using CustomStatusCodes = Core.Common.StatusCodes;
	using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

	[Authorize]
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
				CustomStatusCodes.Ok => StatusCode(StatusCodes.Status200OK, response),
				CustomStatusCodes.NotFound => StatusCode(StatusCodes.Status404NotFound, response),
				CustomStatusCodes.BadRequest => StatusCode(StatusCodes.Status400BadRequest, response),
				CustomStatusCodes.Forbidden => StatusCode(StatusCodes.Status403Forbidden, response),
				CustomStatusCodes.Created => StatusCode(StatusCodes.Status201Created, response),
				CustomStatusCodes.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, response)
			};
		}
	}
}