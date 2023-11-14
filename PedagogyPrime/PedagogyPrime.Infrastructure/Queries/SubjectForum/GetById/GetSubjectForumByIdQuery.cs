using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.SubjectForum;

namespace PedagogyPrime.Infrastructure.Queries.SubjectForums.GetById
{
    public class GetSubjectForumByIdQuery : BaseRequest<BaseResponse<SubjectForumDetails>>
    {
        public Guid Id { get; set; }
    }
}
