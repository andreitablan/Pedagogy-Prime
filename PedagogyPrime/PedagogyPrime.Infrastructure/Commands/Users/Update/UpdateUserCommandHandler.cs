namespace PedagogyPrime.Infrastructure.Commands.Users.Update
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using Models.User;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class UpdateUserCommandHandler : BaseRequestHandler<UpdateUserCommand, BaseResponse<UserDetails>>
	{
		private readonly IUserRepository userRepository;

		public UpdateUserCommandHandler(
			IUserRepository userRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.userRepository = userRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<UserDetails>> Handle(
			UpdateUserCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<UserDetails>.Forbbiden();
				}

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