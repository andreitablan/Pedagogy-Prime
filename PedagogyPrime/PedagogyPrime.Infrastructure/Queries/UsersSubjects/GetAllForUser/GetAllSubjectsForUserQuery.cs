namespace PedagogyPrime.Infrastructure.Queries.UsersSubjects.GetAllForUser
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.Common;
	using PedagogyPrime.Infrastructure.Models.Subject;

	public class GetAllSubjectsForUserQuery : BaseRequest<BaseResponse<List<SubjectDetails>>>
	{
	}
}