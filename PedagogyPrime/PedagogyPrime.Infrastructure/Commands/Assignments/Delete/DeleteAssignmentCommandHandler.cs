namespace PedagogyPrime.Infrastructure.Commands.Assignments.Delete
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteAssignmentCommandHandler : BaseRequestHandler<DeleteAssignmentCommand, BaseResponse<bool>>
	{
		private readonly IAssignmentRepository assignmentRepository;

		public DeleteAssignmentCommandHandler(
            IAssignmentRepository assignmentRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.assignmentRepository = assignmentRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteAssignmentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await assignmentRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Assignment");
				}

				return BaseResponse<bool>.Ok(true);
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}