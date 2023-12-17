using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.SubjectForum;

namespace PedagogyPrime.Infrastructure.Queries.SubjectForums.GetById
{
	using IAuthorization;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using SubjectForum = Core.Entities.SubjectForum;

	public class GetSubjectForumByIdQueryHandler : BaseRequestHandler<GetSubjectForumByIdQuery, BaseResponse<SubjectForumDetails>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public GetSubjectForumByIdQueryHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}

		[HandlerAspect]
		public override async Task<BaseResponse<SubjectForumDetails>> Handle(
			GetSubjectForumByIdQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subjectForum = await subjectForumRepository.GetById(request.Id);

				if(subjectForum == null)
				{
					return BaseResponse<SubjectForumDetails>.NotFound("SubjectForum");
				}

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