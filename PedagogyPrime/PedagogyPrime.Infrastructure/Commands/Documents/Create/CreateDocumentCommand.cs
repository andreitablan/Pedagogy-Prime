using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Create
{
    public class CreateDocumentCommand : BaseRequest<BaseResponse<bool>>
    {
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public DocumentType Type { get; set; } = DocumentType.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
