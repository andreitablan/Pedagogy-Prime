using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Create
{
	using Common;
	using IAuthorization;

	public class CreateCourseCommandHandler : BaseRequestHandler<CreateCourseCommand, BaseResponse<Guid>>
	{
		private readonly ICourseRepository courseRepository;

		public CreateCourseCommandHandler(
			ICourseRepository courseRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}

		public override async Task<BaseResponse<Guid>> Handle(
			CreateCourseCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<Guid>.Forbbiden();
				}

				var course = new Course
				{
					Id = Guid.NewGuid(),
					Name = request.Name,
					Description = request.Description,
					ContentUrl = request.ContentUrl,
					SubjectId = request.SubjectId,
					Index = request.Index,
					IsVisibleForStudents = false
				};

				await courseRepository.Add(course);
				await courseRepository.SaveChanges();

				return BaseResponse<Guid>.Created(course.Id);
			}
			catch
			{
				return BaseResponse<Guid>.InternalServerError();
			}
		}
	}
}