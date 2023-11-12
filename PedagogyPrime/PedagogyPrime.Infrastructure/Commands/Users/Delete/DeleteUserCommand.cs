namespace PedagogyPrime.Infrastructure.Commands.Users.Delete
{
	using Common;
	using Core.Common;

	public class DeleteUserCommand : BaseRequest<BaseResponse<bool>>
	{
		public Guid Id { get; set; }

		public DeleteUserCommand(Guid id)
		{
			Id = id;
		}
	}
}