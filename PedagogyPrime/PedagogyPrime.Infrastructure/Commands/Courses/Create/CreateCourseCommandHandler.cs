using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Create
{
	using Common;
	using IAuthorization;

	public class CreateCourseCommandHandler : BaseRequestHandler<CreateCourseCommand, BaseResponse<bool>>
	{
		private readonly ICourseRepository courseRepository;

		public CreateCourseCommandHandler(
			ICourseRepository courseRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
			CreateCourseCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var course = new Course
				{
					Id = Guid.NewGuid(),
					Name = request.Name,
					Description = request.Description,
					ContentUrl = request.ContentUrl,
					Coverage = request.Coverage,
					SubjectId = request.SubjectId
				};

				await courseRepository.Add(course);
				await courseRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}