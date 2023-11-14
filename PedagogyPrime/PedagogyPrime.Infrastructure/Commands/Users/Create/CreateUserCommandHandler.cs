namespace PedagogyPrime.Infrastructure.Commands.Users.Create
{
	using Common;
	using Core.Entities;
	using Core.IRepositories;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class CreateUserCommandHandler : BaseRequestHandler<CreateUserCommand, BaseResponse<bool>>
	{
		private readonly IUserRepository userRepository;

		public CreateUserCommandHandler(
			IUserRepository userRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.userRepository = userRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
            CreateUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var user = new User
				{
					Id = Guid.NewGuid(),
					FirstName = request.FirstName,
					LastName = request.LastName,
					Password = request.Password,
					Email = request.Email,
					Username = request.Username,
					Role = request.Role
				};

				await userRepository.Add(user);
				await userRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}