using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.User;

namespace PedagogyPrime.Infrastructure.Queries.Users.GetAllUsersNotRegisteredAtSubject
{
    public class GetAllUsersNotRegisteredAtSubjectQueryHandler : BaseRequestHandler<GetAllUsersNotRegisteredAtSubjectQuery, BaseResponse<List<UserDetails>>>
    {
        private readonly IUserSubjectRepository userSubjectRepository;
        private readonly IUserRepository userRepository;

        public GetAllUsersNotRegisteredAtSubjectQueryHandler(IUserAuthorization userAuthorization, IUserSubjectRepository userSubjectRepository, IUserRepository userRepository) : base(userAuthorization)
        {
            this.userSubjectRepository = userSubjectRepository;
            this.userRepository = userRepository;
        }

        public override async Task<BaseResponse<List<UserDetails>>> Handle(GetAllUsersNotRegisteredAtSubjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await userRepository.GetAll();


                var assignedUsers = await userSubjectRepository.GetAllUsersBySubjectId(request.SubjectId);

                var notAssignedUsers = users.Where(x => !assignedUsers.Any(y => y.Id == x.Id)).ToList();

                var usersDetails = notAssignedUsers.Select(GenericMapper<User, UserDetails>.Map).ToList();

                return BaseResponse<List<UserDetails>>.Ok(usersDetails);
            }
            catch
            {
                return BaseResponse<List<UserDetails>>.InternalServerError();
            }
        }
    }
}
