namespace PedagogyPrime.Infrastructure.Queries.Users.GetAllUsersNotRegisteredAtSubject
{
    using Models.User;
    using PedagogyPrime.Core.Common;
    using PedagogyPrime.Infrastructure.Common;

    public class GetAllUsersNotRegisteredAtSubjectQuery : BaseRequest<BaseResponse<List<UserDetails>>>
    {
        public Guid SubjectId { get; set; }

        public GetAllUsersNotRegisteredAtSubjectQuery(Guid subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
