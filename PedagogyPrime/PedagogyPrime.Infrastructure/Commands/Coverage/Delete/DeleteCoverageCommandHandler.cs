namespace PedagogyPrime.Infrastructure.Commands.Coverage.Delete
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using IAuthorization;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class DeleteCoverageCommandHandler : BaseRequestHandler<DeleteCoverageCommand, BaseResponse<bool>>
	{
		private readonly ICoverageRepository coverageRepository;

		public DeleteCoverageCommandHandler(
			IUserAuthorization userAuthorization,
			ICoverageRepository coverageRepository
		) : base(userAuthorization)
		{
			this.coverageRepository = coverageRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteCoverageCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var result = await coverageRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Course");
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