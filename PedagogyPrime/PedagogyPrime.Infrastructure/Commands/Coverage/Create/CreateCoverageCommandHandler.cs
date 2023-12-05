using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Coverage.Create
{
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using System.Threading;
	using System.Threading.Tasks;

	public class CreateCoverageCommandHandler : BaseRequestHandler<CreateCoverageCommand, BaseResponse<Guid>>
	{
		private readonly ICoverageRepository coverageRepository;

		public CreateCoverageCommandHandler(
			IUserAuthorization userAuthorization,
			ICoverageRepository coverageRepository
		) : base(userAuthorization)
		{
			this.coverageRepository = coverageRepository;
		}

		public override async Task<BaseResponse<Guid>> Handle(CreateCoverageCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var coverage = new Coverage
				{
					Id = Guid.NewGuid(),
					GoodWords = request.GoodWords,
					BadWords = request.BadWords,
					CourseId = request.CourseId,
					Percentage = request.Percentage
				};

				await coverageRepository.Add(coverage);
				await coverageRepository.SaveChanges();

				return BaseResponse<Guid>.Ok(coverage.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}