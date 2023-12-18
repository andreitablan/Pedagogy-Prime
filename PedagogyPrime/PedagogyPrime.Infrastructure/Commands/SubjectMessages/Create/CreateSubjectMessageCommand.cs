namespace PedagogyPrime.Infrastructure.Commands.SubjectMessages.Create
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.Common;

	public class CreateSubjectMessageCommand : BaseRequest<BaseResponse<Guid>>
	{
		public Guid Id { get; set; }
		public Guid SubjectId { get; set; }
		public string Text { get; set; }
	}
}