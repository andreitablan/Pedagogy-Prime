using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.UsersSubjects.AddParticipant
{
    public class AddParticipantCommand : BaseRequest<BaseResponse<bool>>
    {
        public List<Guid> UserIds { get; set; }
        public Guid SubjectId { get; set; }
    }
}
