namespace PedagogyPrime.Infrastructure.Commands.SubjectMessage.Create
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;

	public class CreateSubjectMessageCommandHandler : BaseRequestHandler<CreateSubjectMessageCommand, BaseResponse<Guid>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;
		private readonly ISubjectMessageRepository subjectMessageRepository;

		public CreateSubjectMessageCommandHandler(
			IUserAuthorization userAuthorization,
			ISubjectForumRepository subjectForumRepository,
			ISubjectMessageRepository subjectMessageRepository
		) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
			this.subjectMessageRepository = subjectMessageRepository;
		}

		public override async Task<BaseResponse<Guid>> Handle(
			CreateSubjectMessageCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subjectForum = await subjectForumRepository.GetBySubjectId(request.SubjectId);

				if(subjectForum == null)
				{
					return BaseResponse<Guid>.NotFound("Subject Forum");
				}

				var subjectMessage = new SubjectMessage
				{
					Date = DateTime.UtcNow,
					Id = request.Id,
					MessageText = request.Text,
					UserId = request.UserId,
					SubjectForumId = subjectForum.Id
				};

				await subjectMessageRepository.Add(subjectMessage);
				await subjectMessageRepository.SaveChanges();

				return BaseResponse<Guid>.Ok(subjectMessage.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}