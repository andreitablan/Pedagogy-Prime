namespace PedagogyPrime.Infrastructure.Queries.SubjectMessage.GetMessagesForSubject
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.Common;
	using PedagogyPrime.Infrastructure.Models.Message;

	public class GetMessagesForSubject : BaseRequest<BaseResponse<List<MessageDetails>>>
	{
		public Guid SubjectId { get; set; }

		public GetMessagesForSubject(Guid subjectId)
		{
			SubjectId = subjectId;
		}
	}
}