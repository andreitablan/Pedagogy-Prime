namespace PedagogyPrime.Infrastructure.Commands.Users.Delete
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteUserCommandHandler : BaseRequestHandler<DeleteUserCommand, BaseResponse<bool>>
	{
		private readonly IUserRepository userRepository;

		public DeleteUserCommandHandler(
			IUserRepository userRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.userRepository = userRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
			DeleteUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await userRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("User");
				}

				return BaseResponse<bool>.Ok(true);
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}