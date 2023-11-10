using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Create
{
    public class CreateDocumentCommand : BaseRequest<BaseResponse<bool>>
    {
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public TypeDoc Type { get; set; } = TypeDoc.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
