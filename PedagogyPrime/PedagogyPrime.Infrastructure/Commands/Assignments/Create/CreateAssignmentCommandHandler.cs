namespace PedagogyPrime.Infrastructure.Commands.Assignments.Create
{
	using Common;
	using Core.Entities;
	using Core.IRepositories;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class CreateAssignmentCommandHandler : BaseRequestHandler<CreateAssignmentCommand, BaseResponse<Guid>>
	{
		private readonly IAssignmentRepository assignmentRepository;

		public CreateAssignmentCommandHandler(
            IAssignmentRepository assignmentRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.assignmentRepository = assignmentRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<Guid>> Handle(
			CreateAssignmentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<Guid>.Forbbiden();
				}

				var assignment = new Assignment
				{
					Id = Guid.NewGuid(),
					Deadline = request.Deadline,
					Description = request.Description,
					CourseId = request.CourseId,
                 };

				await assignmentRepository.Add(assignment);
				await assignmentRepository.SaveChanges();

				return BaseResponse<Guid>.Created(assignment.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}