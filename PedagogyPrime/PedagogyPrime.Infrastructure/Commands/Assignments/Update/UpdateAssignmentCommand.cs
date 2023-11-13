namespace PedagogyPrime.Infrastructure.Commands.Assignments.Update
{
	using Common;
	using Core.Common;
	using Models.User;
	using PedagogyPrime.Core.Entities;
	using PedagogyPrime.Infrastructure.Models.Assignment;
	using System.Text.Json.Serialization;

	public class UpdateAssignmentCommand : BaseRequest<BaseResponse<AssignmentDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }

        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public List<Homework> Homeworks { get; set; }
        public Guid SolutionId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}