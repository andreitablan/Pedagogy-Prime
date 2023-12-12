namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Delete
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteHomeworkCommandHandler : BaseRequestHandler<DeleteHomeworkCommand, BaseResponse<bool>>
	{
		private readonly IHomeworkRepository homeworkRepository;

		public DeleteHomeworkCommandHandler(
            IHomeworkRepository homeworkRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.homeworkRepository = homeworkRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteHomeworkCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await homeworkRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Homework");
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