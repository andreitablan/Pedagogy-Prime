namespace PedagogyPrime.Infrastructure.Commands.Coverage.Update
{
	using Common;
	using Core.Common;
    using PedagogyPrime.Infrastructure.Models.Coverage;

    public class UpdateCoverageCommand : BaseRequest<BaseResponse<CoverageDetails>>
	{
		public Guid Id { get; set; }
		public List<string> GoodWords { get; set; }
		public List<string> BadWords { get; set; }
		public Double Precentage { get; set; }
		public Guid CourseId { get; set; }
	}
}