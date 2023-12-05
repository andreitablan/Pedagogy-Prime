namespace PedagogyPrime.Infrastructure.Queries.UsersSubjects.GetAllForUser
{
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Core.Entities;
	using PedagogyPrime.Core.IRepositories;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;
	using PedagogyPrime.Infrastructure.Models.Subject;

	public class GetAllSubjectsForUserQueryHandler : BaseRequestHandler<GetAllSubjectsForUserQuery, BaseResponse<List<SubjectDetails>>>
	{
		private readonly IUserSubjectRepository userSubjectRepository;

		public GetAllSubjectsForUserQueryHandler(
			IUserAuthorization userAuthorization,
			IUserSubjectRepository userSubjectRepository
		) : base(userAuthorization)
		{
			this.userSubjectRepository = userSubjectRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<List<SubjectDetails>>> Handle(
			GetAllSubjectsForUserQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subjects = await userSubjectRepository.GetAllSubjectsForUser(request.UserId);

				var subjectsDetails = subjects.Select(GenericMapper<Subject, SubjectDetails>.Map).ToList();

				return BaseResponse<List<SubjectDetails>>.Ok(subjectsDetails);
			}
			catch
			{
				return BaseResponse<List<SubjectDetails>>.InternalServerError();
			}
		}
	}
}