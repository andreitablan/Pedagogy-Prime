using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.SubjectForum.GetById
{
    public class GetSubjectForumByIdQuery : BaseRequest<BaseResponse<SubjectForumDetails>>
    {
        public Guid Id { get; set; }
    }
}
