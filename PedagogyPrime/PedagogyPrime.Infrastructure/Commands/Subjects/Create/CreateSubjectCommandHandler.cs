using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Create
{
	using Common;

	public class CreateSubjectCommandHandler : BaseRequestHandler<CreateSubjectCommand, BaseResponse<Guid>>
	{
		private readonly ISubjectRepository subjectRepository;

		public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}

		public override async Task<BaseResponse<Guid>> Handle(
            CreateSubjectCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<Guid>.Forbbiden();
				}

				var subject = new Subject
				{
					Id = Guid.NewGuid(),
                    Name = request.Name,
                    Period = request.Period,
                    NoOfCourses = request.NoOfCourses,
				};

				await subjectRepository.Add(subject);
				await subjectRepository.SaveChanges();

				return BaseResponse<Guid>.Created(subject.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}