using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Delete
{
    public class DeleteSubjectCommand : BaseRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
