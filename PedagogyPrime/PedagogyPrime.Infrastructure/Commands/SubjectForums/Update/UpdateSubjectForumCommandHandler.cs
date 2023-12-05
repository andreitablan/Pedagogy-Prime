using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.SubjectForum;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForums.Update
{
	using IAuthorization;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class UpdateSubjectForumCommandHandler : BaseRequestHandler<UpdateSubjectForumCommand, BaseResponse<SubjectForumDetails>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public UpdateSubjectForumCommandHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<SubjectForumDetails>> Handle(
            UpdateSubjectForumCommand request,
			CancellationToken cancellationToken
	   )
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<SubjectForumDetails>.Forbbiden();
				}

				var subjectForum = await subjectForumRepository.GetById(request.Id);

				if(subjectForum == null)
				{
					return BaseResponse<SubjectForumDetails>.NotFound("SubjectForum");
				}

                subjectForum.Id = subjectForum.Id;
                subjectForum.SubjectId = request.SubjectId;

				await subjectForumRepository.SaveChanges();

				var subjectForumDetails = GenericMapper<SubjectForum, SubjectForumDetails>.Map(subjectForum);

				return BaseResponse<SubjectForumDetails>.Ok(subjectForumDetails);
			}
			catch
			{
				return BaseResponse<SubjectForumDetails>.InternalServerError();
			}
		}
	}
}