namespace PedagogyPrime.Infrastructure.Queries.Users.GetById
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using MediatR;
	using Models.User;

	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserDetails>>
	{
		private readonly IUserRepository userRepository;

		public GetUserByIdQueryHandler(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<BaseResponse<UserDetails>> Handle(
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