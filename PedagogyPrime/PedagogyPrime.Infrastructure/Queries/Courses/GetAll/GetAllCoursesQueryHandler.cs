using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetAll
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, BaseResponse<List<CourseDetails>>>
    {
        private readonly ICourseRepository courseRepository;
        public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<BaseResponse<List<CourseDetails>>> Handle(
            GetAllCoursesQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var courses = await courseRepository.GetAll();

                var courseDetails = courses.Select(GenericMapper<Course, CourseDetails>.Map).ToList();

                return BaseResponse<List<CourseDetails>>.Ok(courseDetails);
            }
            catch (Exception e)
            {
                return BaseResponse<List<CourseDetails>>.BadRequest(
                    new List<string>
                    {
                        e.Message
                    }
                );
            }
        }
    }
}
