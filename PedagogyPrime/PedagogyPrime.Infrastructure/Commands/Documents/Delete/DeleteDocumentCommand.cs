using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Delete
{
    public class DeleteDocumentCommand : BaseRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
