using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Subject;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Update
{
	using IAuthorization;

	public class UpdateSubjectCommandHandler : BaseRequestHandler<UpdateSubjectCommand, BaseResponse<SubjectDetails>>
	{
		private readonly ISubjectRepository subjectRepository;

		public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}

		public override async Task<BaseResponse<SubjectDetails>> Handle(
			UpdateSubjectCommand request,
			CancellationToken cancellationToken
	   )
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<SubjectDetails>.Forbbiden();
				}

				var subject = await subjectRepository.GetById(request.Id);

				if(subject == null)
				{
					return BaseResponse<SubjectDetails>.NotFound("Subject");
				}

                subject.Id = subject.Id;
                subject.Name = request.Name;
                subject.Period = request.Period;
                subject.NoOfCourses = request.NoOfCourses;

				await subjectRepository.SaveChanges();

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