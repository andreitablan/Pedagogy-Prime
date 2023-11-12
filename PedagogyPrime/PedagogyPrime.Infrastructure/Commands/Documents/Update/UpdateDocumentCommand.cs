using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Update
{
    public class UpdateDocumentCommand : BaseRequest<BaseResponse<DocumentDetails>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ContentUrl { get; set; } = String.Empty;
        public State State { get; set; }
        public DocumentType Type { get; set; } = DocumentType.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
