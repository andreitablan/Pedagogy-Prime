using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Subject;

namespace PedagogyPrime.Infrastructure.Queries.Subjects.GetById
{
	using IAuthorization;
	using Models.Course;

	public class GetSubjectByIdQueryHandler : BaseRequestHandler<GetSubjectByIdQuery, BaseResponse<SubjectInfo>>
	{
		private readonly ISubjectRepository subjectRepository;

		public GetSubjectByIdQueryHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}

		public override async Task<BaseResponse<SubjectInfo>> Handle(
			GetSubjectByIdQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var subject = await subjectRepository.GetById(request.Id);

				if(subject == null)
				{
					return BaseResponse<SubjectInfo>.NotFound("Subject");
				}

				var subjectDetails = GenericMapper<Subject, SubjectInfo>.Map(subject);

				subjectDetails.CoursesDetails = subject.Courses.Select(GenericMapper<Course, CourseDetails>.Map).OrderBy(x => x.Index).ToList();

				return BaseResponse<SubjectInfo>.Ok(subjectDetails);
			}
			catch
			{
				return BaseResponse<SubjectInfo>.InternalServerError();
			}
		}
	}
}