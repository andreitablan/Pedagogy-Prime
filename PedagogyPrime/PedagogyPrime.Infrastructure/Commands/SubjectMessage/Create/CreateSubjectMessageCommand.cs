namespace PedagogyPrime.Infrastructure.Commands.SubjectMessage.Create
{
	using Common;
	using Core.Common;

	public class CreateSubjectMessageCommand : BaseRequest<BaseResponse<Guid>>
	{
		public Guid Id { get; set; }
		public Guid SubjectId { get; set; }
		public string Text { get; set; }
	}
}