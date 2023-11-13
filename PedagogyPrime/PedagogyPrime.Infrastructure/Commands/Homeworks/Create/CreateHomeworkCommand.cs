namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Create
{
	using Common;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;

	public class CreateHomeworkCommand : BaseRequest<BaseResponse<bool>>
	{
        public Grade Grade { get; set; }
        public string Review { get; set; }
        public string ContentUrl { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}