namespace PedagogyPrime.Infrastructure.Commands.Users.Update
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using MediatR;
	using Models.User;

	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDetails>>
	{
		private readonly IUserRepository userRepository;

		public UpdateUserCommandHandler(
			IUserRepository userRepository
		)
		{
			this.userRepository = userRepository;
		}

		public async Task<BaseResponse<UserDetails>> Handle(
			UpdateUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var user = await userRepository.GetById(request.Id);

				if(user == null)
				{
					return BaseResponse<UserDetails>.NotFound("User");
				}

				user.FirstName = request.FirstName;
				user.LastName = request.LastName;
				user.Role = request.Role;

				await userRepository.SaveChanges();

				var userDetails = GenericMapper<User, UserDetails>.Map(user);

				return BaseResponse<UserDetails>.Ok(userDetails);
			}
			catch
			{
				return BaseResponse<UserDetails>.InternalServerError();
			}
		}
	}
}