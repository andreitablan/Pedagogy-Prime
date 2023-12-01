namespace PedagogyPrime.Infrastructure.Commands.Coverage.Create
{
	using Common;
	using Core.Common;

	public class CreateCoverageCommand : BaseRequest<BaseResponse<Guid>>
	{
		public List<string> GoodWords { get; set; }
		public List<string> BadWords { get; set; }
		public Double Precentage { get; set; }
		public Guid CourseId { get; set; }
	}
}