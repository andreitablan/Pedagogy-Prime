using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForums.Delete
{
    public class DeleteSubjectForumCommand : BaseRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
