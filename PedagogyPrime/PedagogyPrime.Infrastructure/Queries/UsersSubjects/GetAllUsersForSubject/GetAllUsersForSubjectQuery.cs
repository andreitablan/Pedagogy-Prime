namespace PedagogyPrime.Infrastructure.Queries.UsersSubjects.GetAllUsersForSubject
{
    using Common;
    using Core.Common;
    using Models.User;
    public class GetAllUsersForSubjectQuery : BaseRequest<BaseResponse<List<UserDetails>>>
    {
        public Guid SubjectId { get; set; }
    }
}
