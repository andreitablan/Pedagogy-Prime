namespace PedagogyPrime.Infrastructure.Commands.Assignments.Delete
{
	using Common;
	using Core.Common;

	public class DeleteAssignmentCommand : BaseRequest<BaseResponse<bool>>
	{
		public Guid Id { get; set; }

		public DeleteAssignmentCommand(Guid id)
		{
			Id = id;
		}
	}
}