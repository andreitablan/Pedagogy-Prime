using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Subjects.GetAll
{
	using IAuthorization;

	public class GetAllSubjectsQueryHandler : BaseRequestHandler<GetAllSubjectsQuery, BaseResponse<List<SubjectDetails>>>
	{
		private readonly ISubjectRepository subjectRepository;

		public GetAllDocumentsQueryHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}

		public override async Task<BaseResponse<List<SubjectDetails>>> Handle(
			GetAllSubjectsQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<List<SubjectDetails>>.Forbbiden();
				}

				var subjects = await subjectRepository.GetAll();

				var subjectsDetails = subject.Select(GenericMapper<Subject, SubjectDetails>.Map).ToList();

				return BaseResponse<List<SubjectDetails>>.Ok(subjectsDetails);
			}
			catch
			{
				return BaseResponse<List<DocumentDetails>>.InternalServerError();
			}
		}
	}
}