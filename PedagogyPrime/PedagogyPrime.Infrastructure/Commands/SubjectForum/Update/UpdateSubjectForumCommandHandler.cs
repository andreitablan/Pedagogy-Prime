using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Update
{
	using IAuthorization;

	public class UpdateSubjectForumCommandHandler : BaseRequestHandler<UpdateSubjectForumCommand, BaseResponse<SubjectForumDetails>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public UpdateSubjectForumCommandHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}

		public override async Task<BaseResponse<SubjectDetails>> Handle(
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

				if(subject == null)
				{
					return BaseResponse<SubjectForumDetails>.NotFound("SubjectForum");
				}

                subject.Id = subject.Id;
                subject.SubjectId = request.SubjectId;

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