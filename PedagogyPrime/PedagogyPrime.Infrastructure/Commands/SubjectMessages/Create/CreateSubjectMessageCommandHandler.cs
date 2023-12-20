namespace PedagogyPrime.Infrastructure.Commands.SubjectMessages.Create
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;
	using PedagogyPrime.Core.IRepositories;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;

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

		[HandlerAspect]
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