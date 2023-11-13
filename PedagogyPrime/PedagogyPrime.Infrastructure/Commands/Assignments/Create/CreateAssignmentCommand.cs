namespace PedagogyPrime.Infrastructure.Commands.Assignments.Create
{
	using Common;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;

	public class CreateAssignmentCommand : BaseRequest<BaseResponse<bool>>
	{
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
    }
}