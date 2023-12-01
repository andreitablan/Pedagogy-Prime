namespace PedagogyPrime.Infrastructure.Commands.Coverage.Delete
{
	using Common;
	using Core.Common;

	public class DeleteCoverageCommand : BaseRequest<BaseResponse<bool>>
	{
		public Guid Id { get; set; }
	}
}