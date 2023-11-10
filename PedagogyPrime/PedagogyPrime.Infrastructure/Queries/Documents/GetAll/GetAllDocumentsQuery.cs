using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetAll
{
    public class GetAllDocumentsQuery : BaseRequest<BaseResponse<List<DocumentDetails>>>
    {
    }
}
