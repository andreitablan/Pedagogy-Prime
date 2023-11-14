using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.SubjectForum;

namespace PedagogyPrime.Infrastructure.Queries.SubjectForums.GetAll
{
    public class GetAllSubjectForumQuery : BaseRequest<BaseResponse<List<SubjectForumDetails>>>
    {
    }
}
