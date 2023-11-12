namespace PedagogyPrime.Infrastructure.Queries.Users.GetAll
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using Models.User;

	public class GetAllUsersQueryHandler : BaseRequestHandler<GetAllUsersQuery, BaseResponse<List<UserDetails>>>
	{
		private readonly IUserRepository userRepository;

		public GetAllUsersQueryHandler(IUserRepository userRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.userRepository = userRepository;
		}

		public override async Task<BaseResponse<List<UserDetails>>> Handle(
			GetAllUsersQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<List<UserDetails>>.Forbbiden();
				}

				var users = await userRepository.GetAll();

				var usersDetails = users.Select(GenericMapper<User, UserDetails>.Map).ToList();

				return BaseResponse<List<UserDetails>>.Ok(usersDetails);
			}
			catch
			{
				return BaseResponse<List<UserDetails>>.InternalServerError();
			}
		}
	}
}