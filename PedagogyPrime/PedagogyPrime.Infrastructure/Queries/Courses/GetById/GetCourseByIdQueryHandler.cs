using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetById
{
	public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, BaseResponse<CourseDetails>>
	{
		private readonly ICourseRepository courseRepository;

		public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
		{
			this.courseRepository = courseRepository;
		}

		public async Task<BaseResponse<CourseDetails>> Handle(
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