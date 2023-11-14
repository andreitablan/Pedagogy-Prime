using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForum.Create
{
	using Common;

	public class CreateSubjectForumCommandHandler : BaseRequestHandler<CreateSubjectForumCommand, BaseResponse<bool>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public CreateSubjectCommandHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
            CreateSubjectForumCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var subjectForum = new Subject
				{
					Id = Guid.NewGuid(),
				};

				await subjectForumRepository.Add(subjectForum);
				await subjectForumRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}