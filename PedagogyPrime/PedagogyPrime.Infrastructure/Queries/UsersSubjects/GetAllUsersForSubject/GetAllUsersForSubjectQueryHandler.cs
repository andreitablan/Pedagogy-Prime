namespace PedagogyPrime.Infrastructure.Queries.UsersSubjects.GetAllUsersForSubject
{
    using PedagogyPrime.Core.Common;
    using PedagogyPrime.Core.Entities;
    using PedagogyPrime.Core.IRepositories;
    using PedagogyPrime.Infrastructure.Common;
    using PedagogyPrime.Infrastructure.IAuthorization;
    using PedagogyPrime.Infrastructure.Models.User;

    public class GetAllUsersForSubjectQueryHandler : BaseRequestHandler<GetAllUsersForSubjectQuery, BaseResponse<List<UserDetails>>>
    {
        private readonly IUserSubjectRepository userSubjectRepository;

        public GetAllUsersForSubjectQueryHandler(IUserAuthorization userAuthorization, IUserSubjectRepository userSubjectRepository) : base(userAuthorization)
        {
            this.userSubjectRepository = userSubjectRepository;
        }

        public override async Task<BaseResponse<List<UserDetails>>> Handle(
            GetAllUsersForSubjectQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                if (!(await IsAuthorized(request.UserId)))
                {
                    return BaseResponse<List<UserDetails>>.Forbbiden();
                }

                var users = await userSubjectRepository.GetAllUsersBySubjectId(request.SubjectId);

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
