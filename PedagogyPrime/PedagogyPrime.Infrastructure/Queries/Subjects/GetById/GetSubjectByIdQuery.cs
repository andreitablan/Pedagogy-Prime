using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Subjects.GetById
{
    public class GetSubjectByIdQuery : BaseRequest<BaseResponse<SubjectDetails>>
    {
        public Guid Id { get; set; }
    }
}
