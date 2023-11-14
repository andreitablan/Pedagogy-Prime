using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForum.Delete
{
    public class DeleteSubjectForumCommand : BaseRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
