using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetAll
{
	using IAuthorization;

	public class GetAllCoursesQueryHandler : BaseRequestHandler<GetAllCoursesQuery, BaseResponse<List<CourseDetails>>>
	{
		private readonly ICourseRepository courseRepository;

		public GetAllCoursesQueryHandler(ICourseRepository courseRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}

		public override async Task<BaseResponse<List<CourseDetails>>> Handle(
			GetAllCoursesQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<List<CourseDetails>>.Forbbiden();
				}

				var courses = await courseRepository.GetAll();

				var courseDetails = courses.Select(GenericMapper<Course, CourseDetails>.Map).ToList();

				return BaseResponse<List<CourseDetails>>.Ok(courseDetails);
			}
			catch(Exception e)
			{
				return BaseResponse<List<CourseDetails>>.InternalServerError();
			}
		}
	}
}