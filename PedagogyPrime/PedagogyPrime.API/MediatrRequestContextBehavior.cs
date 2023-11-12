namespace PedagogyPrime.API
{
	using Infrastructure.Common;
	using MediatR;
	using Microsoft.AspNetCore.Authorization;
	using System.Security.Claims;

	public class MediatrRequestContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : BaseRequest<TResponse>

	{
		private readonly IHttpContextAccessor _contextAccessor;

		public MediatrRequestContextBehaviour(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			var endpoint = _contextAccessor.HttpContext.GetEndpoint();

			if(endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null)
			{
				return await next();
			}

			var userIdClaim =
				_contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

			if(userIdClaim == null)
			{
				throw new UnauthorizedAccessException("User doesn't have the necessary claims");
			}

			if(Guid.TryParse(userIdClaim.Value, out var userId))
			{
				request.UserId = userId;
			}

			var response = await next();
			return response;
		}
	}
}