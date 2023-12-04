﻿namespace PedagogyPrime.Infrastructure.Commands.Coverage.Update
{
	using Common;
	using Core.Common;
	using Core.IRepositories;
	using IAuthorization;
    using PedagogyPrime.Core.Entities;
    using PedagogyPrime.Infrastructure.Models.Course;
    using PedagogyPrime.Infrastructure.Models.Coverage;

    public class UpdateCoverageCommandHandler : BaseRequestHandler<UpdateCoverageCommand, BaseResponse<CoverageDetails>>
	{
		private readonly ICoverageRepository coverageRepository;

		public UpdateCoverageCommandHandler(
			IUserAuthorization userAuthorization,
			ICoverageRepository coverageRepository
		) : base(userAuthorization)
		{
			this.coverageRepository = coverageRepository;
		}

		public override async Task<BaseResponse<CoverageDetails>> Handle(
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

                var coverageDetails = GenericMapper<Coverage, CoverageDetails>.Map(coverage);

                return BaseResponse<CoverageDetails>.Ok(coverageDetails);
			}
			catch
			{
				return BaseResponse<CoverageDetails>.InternalServerError();
			}
		}
	}
}