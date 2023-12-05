using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForums.Create
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class CreateSubjectForumCommandHandler : BaseRequestHandler<CreateSubjectForumCommand, BaseResponse<Guid>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public CreateSubjectForumCommandHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<Guid>> Handle(
            CreateSubjectForumCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<Guid>.Forbbiden();
				}

				var subjectForum = new SubjectForum
				{
					Id = Guid.NewGuid(),
					SubjectId = request.SubjectId
				};

				await subjectForumRepository.Add(subjectForum);
				await subjectForumRepository.SaveChanges();

				return BaseResponse<Guid>.Created(subjectForum.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}