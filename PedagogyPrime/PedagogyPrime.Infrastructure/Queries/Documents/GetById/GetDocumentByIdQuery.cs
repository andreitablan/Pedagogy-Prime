using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetById
{
    public class GetDocumentByIdQuery : BaseRequest<BaseResponse<DocumentDetails>>
    {
        public Guid Id { get; set; }
    }
}
