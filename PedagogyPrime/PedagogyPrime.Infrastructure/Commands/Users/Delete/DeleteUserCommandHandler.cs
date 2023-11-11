namespace PedagogyPrime.Infrastructure.Commands.Users.Delete
{
	using Core.Common;
	using Core.IRepositories;
	using MediatR;

	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
	{
		private readonly IUserRepository userRepository;

		public DeleteUserCommandHandler(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<BaseResponse<bool>> Handle(
			DeleteUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
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