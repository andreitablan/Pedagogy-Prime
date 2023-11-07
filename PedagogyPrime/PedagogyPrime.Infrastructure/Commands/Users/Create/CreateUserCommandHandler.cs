namespace PedagogyPrime.Infrastructure.Commands.Users.Create
{
	using Core.Entities;
	using Core.IRepositories;
	using MediatR;
	using PedagogyPrime.Core.Common;

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
	{
		private readonly IUserRepository userRepository;

		public CreateUserCommandHandler(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<BaseResponse<bool>> Handle(
			CreateUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
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
			catch(Exception e)
			{
				return BaseResponse<bool>.BadRequest(new List<string>
				{
					e.Message
				});
			}
		}
	}
}