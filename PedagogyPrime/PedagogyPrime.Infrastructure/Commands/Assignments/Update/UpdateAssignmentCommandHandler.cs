namespace PedagogyPrime.Infrastructure.Commands.Assignments.Update
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using Models.User;
	using PedagogyPrime.Infrastructure.Models.Assignment;

	public class UpdateAssignmentCommandHandler : BaseRequestHandler<UpdateAssignmentCommand, BaseResponse<AssignmentDetails>>
	{
		private readonly IAssignmentRepository assignmentRepository;

        public UpdateAssignmentCommandHandler(
            IAssignmentRepository assignmentRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.assignmentRepository = assignmentRepository;
		}

		public override async Task<BaseResponse<AssignmentDetails>> Handle(
			UpdateAssignmentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<AssignmentDetails>.Forbbiden();
				}

				var assignment = await assignmentRepository.GetById(request.Id);

				if(assignment == null)
				{
					return BaseResponse<AssignmentDetails>.NotFound("Assignment");
				}
				assignment.Deadline = request.Deadline;
				assignment.Description = request.Description;
				assignment.Homeworks = request.Homeworks;
				assignment.SolutionId = request.SolutionId;
				assignment.CourseId = request.CourseId;
				assignment.Course = request.Course;

				await assignmentRepository.SaveChanges();

				var assignmentDetails = GenericMapper<Assignment, AssignmentDetails>.Map(assignment);

				return BaseResponse<AssignmentDetails>.Ok(assignmentDetails);
			}
			catch
			{
				return BaseResponse<AssignmentDetails>.InternalServerError();
			}
		}
	}
}