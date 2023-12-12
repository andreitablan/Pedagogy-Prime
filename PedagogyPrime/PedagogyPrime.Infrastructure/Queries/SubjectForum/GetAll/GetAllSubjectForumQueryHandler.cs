using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.SubjectForum;

namespace PedagogyPrime.Infrastructure.Queries.SubjectForums.GetAll
{
	using IAuthorization;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class GetAllSubjectForumQueryHandler : BaseRequestHandler<GetAllSubjectForumQuery, BaseResponse<List<SubjectForumDetails>>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public GetAllSubjectForumQueryHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<List<SubjectForumDetails>>> Handle(
            GetAllSubjectForumQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<List<SubjectForumDetails>>.Forbbiden();
				}

				var subjectForums = await subjectForumRepository.GetAll();

				var subjectForumsDetails = subjectForums.Select(GenericMapper<SubjectForum, SubjectForumDetails>.Map).ToList();

				return BaseResponse<List<SubjectForumDetails>>.Ok(subjectForumsDetails);
			}
			catch
			{
				return BaseResponse<List<SubjectForumDetails>>.InternalServerError();
			}
		}
	}
}