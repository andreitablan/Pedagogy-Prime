namespace PedagogyPrime.Infrastructure.Commands.Assignments.Create
{
	using Common;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;

	public class CreateAssignmentCommand : BaseRequest<BaseResponse<bool>>
	{
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public List<Homework> Homeworks { get; set; }
        public Guid SolutionId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}