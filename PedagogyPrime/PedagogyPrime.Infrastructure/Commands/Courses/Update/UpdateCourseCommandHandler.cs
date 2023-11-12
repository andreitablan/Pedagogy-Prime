using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Update
{
	public class UpdateCourseCommandHandler : BaseRequestHandler<UpdateCourseCommand, BaseResponse<CourseDetails>>
	{
		private readonly ICourseRepository courseRepository;

		public UpdateCourseCommandHandler(
			ICourseRepository courseRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}

		public override async Task<BaseResponse<CourseDetails>> Handle(
		   UpdateCourseCommand request,
		   CancellationToken cancellationToken
	   )
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<CourseDetails>.Forbbiden();
				}

				var course = await courseRepository.GetById(request.Id);

				if(course == null)
				{
					return BaseResponse<CourseDetails>.NotFound("Course");
				}

				course.Name = request.Name;
				course.Description = request.Description;
				course.ContentUrl = request.ContentUrl;
				course.Coverage = request.Coverage;

				await courseRepository.SaveChanges();

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