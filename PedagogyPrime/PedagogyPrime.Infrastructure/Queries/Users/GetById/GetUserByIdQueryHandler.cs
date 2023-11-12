namespace PedagogyPrime.Infrastructure.Queries.Users.GetById
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using Models.User;

	public class GetUserByIdQueryHandler : BaseRequestHandler<GetUserByIdQuery, BaseResponse<UserDetails>>
	{
		private readonly IUserRepository userRepository;


		public GetUserByIdQueryHandler(IUserRepository userRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.userRepository = userRepository;
		}

		public override async Task<BaseResponse<UserDetails>> Handle(
			GetUserByIdQuery request,
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

				if(!(await IsAuthorized(request.UserId, user.Id)))
				{
					return BaseResponse<UserDetails>.Forbbiden();
				}

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