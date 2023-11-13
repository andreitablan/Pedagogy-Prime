namespace PedagogyPrime.Infrastructure.Commands.Assignments.Create
{
	using Common;
	using Core.Entities;
	using Core.IRepositories;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class CreateAssignmentCommandHandler : BaseRequestHandler<CreateAssignmentCommand, BaseResponse<bool>>
	{
		private readonly IAssignmentRepository assignmentRepository;

		public CreateAssignmentCommandHandler(
            IAssignmentRepository assignmentRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.assignmentRepository = assignmentRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
			CreateAssignmentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var assignment = new Assignment
				{
					Id = Guid.NewGuid(),
					Deadline = request.Deadline,
					Description = request.Description,
					Homeworks = request.Homeworks,
					SolutionId = request.SolutionId,
					CourseId = request.CourseId,
					Course = request.Course
                 };

				await assignmentRepository.Add(assignment);
				await assignmentRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}