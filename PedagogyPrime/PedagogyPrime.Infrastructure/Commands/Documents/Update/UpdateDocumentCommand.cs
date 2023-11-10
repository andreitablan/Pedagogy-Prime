using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Update
{
    public class UpdateDocumentCommand : BaseRequest<BaseResponse<DocumentDetails>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public TypeDoc Type { get; set; } = TypeDoc.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
