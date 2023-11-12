namespace PedagogyPrime.Infrastructure.Common
{
	using IAuthorization;
	using MediatR;

	public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	{
		private readonly IUserAuthorization userAuthorization;

		public BaseRequestHandler(IUserAuthorization userAuthorization)
		{
			this.userAuthorization = userAuthorization;
		}

		public virtual Task<TResponse> Handle(
			TRequest request,
			CancellationToken cancellationToken
		)
		{
			throw new NotImplementedException();
		}

		protected async Task<bool> IsAuthorized(
			Guid userId,
			Guid resourceUserId
		)
		{
			return userAuthorization.IsRequestForItself(userId, resourceUserId) ||
				   await userAuthorization.IsAdmin(userId);
		}

		protected async Task<bool> IsAuthorized(Guid userId)
		{
			return await userAuthorization.IsAdmin(userId);
		}
	}
}
