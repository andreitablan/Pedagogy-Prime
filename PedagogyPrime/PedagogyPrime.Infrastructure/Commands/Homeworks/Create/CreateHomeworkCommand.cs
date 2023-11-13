namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Create
{
	using Common;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;

	public class CreateHomeworkCommand : BaseRequest<BaseResponse<bool>>
	{
        public string Review { get; set; }
        public string ContentUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid AssignmentId { get; set; }
    }
}