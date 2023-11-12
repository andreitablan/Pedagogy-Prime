using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetById
{
	public class GetCourseByIdQueryHandler : BaseRequestHandler<GetCourseByIdQuery, BaseResponse<CourseDetails>>
	{
		private readonly ICourseRepository courseRepository;

		public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}

		public override async Task<BaseResponse<CourseDetails>> Handle(
			GetCourseByIdQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var course = await courseRepository.GetById(request.Id);

				if(course == null)
				{
					return BaseResponse<CourseDetails>.NotFound("Course");
				}

				if(!(await IsAuthorized(request.UserId, course.SubjectId)))
				{
					return BaseResponse<CourseDetails>.Forbbiden();
				}

				var courseDetails = GenericMapper<Course, CourseDetails>.Map(course);

				return BaseResponse<CourseDetails>.Ok(courseDetails);
			}
			catch
			{
				return BaseResponse<CourseDetails>.InternalServerError();
			}
		}
	}
}