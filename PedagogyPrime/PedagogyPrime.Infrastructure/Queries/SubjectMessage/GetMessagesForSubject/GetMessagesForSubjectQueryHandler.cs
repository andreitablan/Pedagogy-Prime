namespace PedagogyPrime.Infrastructure.Queries.SubjectMessage.GetMessagesForSubject
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.IRepositories;
	using PedagogyPrime.Infrastructure.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;
	using PedagogyPrime.Infrastructure.Models.Message;

	public class GetMessagesForSubjectQueryHandler : BaseRequestHandler<GetMessagesForSubject, BaseResponse<List<MessageDetails>>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;
		private readonly ISubjectMessageRepository subjectMessageRepository;

		public GetMessagesForSubjectQueryHandler(
			IUserAuthorization userAuthorization,
			ISubjectForumRepository subjectForumRepository,
			ISubjectMessageRepository subjectMessageRepository
		) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
			this.subjectMessageRepository = subjectMessageRepository;
		}

		public override async Task<BaseResponse<List<MessageDetails>>> Handle(
			GetMessagesForSubject request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subjectForum = await subjectForumRepository.GetBySubjectId(request.SubjectId);

				if(subjectForum == null)
				{
					return BaseResponse<List<MessageDetails>>.NotFound("Subject Forum");
				}

				var messages = await subjectMessageRepository.GetAllBySubjectForumId(subjectForum.Id);

				var messageDetails = messages.Select(x => new MessageDetails
				{
					Date = x.Date,
					MessageText = x.MessageText,
					UserId = x.UserId,
					Username = x.User.Username
				}).ToList();

				return BaseResponse<List<MessageDetails>>.Ok(messageDetails);
			}
			catch
			{
				return BaseResponse<List<MessageDetails>>.InternalServerError();
			}
		}
	}
}