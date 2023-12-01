namespace PedagogyPrime.Infrastructure.Commands.Coverage.Update
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using IAuthorization;

	public class UpdateCoverageCommandHandler : BaseRequestHandler<UpdateCoverageCommand, BaseResponse<bool>>
	{
		private readonly ICoverageRepository coverageRepository;

		public UpdateCoverageCommandHandler(
			IUserAuthorization userAuthorization,
			ICoverageRepository coverageRepository
		) : base(userAuthorization)
		{
			this.coverageRepository = coverageRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
			UpdateCoverageCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var coverage = await coverageRepository.GetById(request.Id);

				coverage.GoodWords = request.GoodWords;
				coverage.BadWords = request.BadWords;
				coverage.Percentage = request.Precentage;

				await coverageRepository.SaveChanges();

				return BaseResponse<bool>.Ok(true);
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}