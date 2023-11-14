using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Subjects.GetById
{
	using IAuthorization;

	public class GetSubjectByIdQueryHandler : BaseRequestHandler<GetSubjectByIdQuery, BaseResponse<SubjectDetails>>
	{
		private readonly ISubjectRepository subjectRepository;

		public GetSubjectByIdQueryHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}

		public override async Task<BaseResponse<SubjectDetails>> Handle(
            GetSubjectByIdQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subject = await subjectRepository.GetById(request.Id);

				if(subject == null)
				{
					return BaseResponse<DocumentDetails>.NotFound("Subject");
				}

				var subjectDetails = GenericMapper<Subject, SubjectDetails>.Map(subject);

				return BaseResponse<SubjectDetails>.Ok(subjectDetails);
			}
			catch
			{
				return BaseResponse<SubjectDetails>.InternalServerError();
			}
		}
	}
}