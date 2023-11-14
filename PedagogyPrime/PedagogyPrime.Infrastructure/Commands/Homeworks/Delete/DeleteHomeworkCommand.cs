namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Delete
{
	using Common;
	using Core.Common;

	public class DeleteHomeworkCommand : BaseRequest<BaseResponse<bool>>
	{
		public Guid Id { get; set; }

		public DeleteHomeworkCommand(Guid id)
		{
			Id = id;
		}
	}
}