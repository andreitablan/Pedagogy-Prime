using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Commands.Courses.Create;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Update
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, BaseResponse<CourseDetails>>
    {
        private readonly ICourseRepository courseRepository;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<BaseResponse<CourseDetails>> Handle(
           UpdateCourseCommand request,
           CancellationToken cancellationToken
       )
        {
            try
            {
                var oldCourse = await courseRepository.GetById(request.Id);
                if (oldCourse == null)
                {
                    return BaseResponse<CourseDetails>.NotFound();
                }

                var updatedCourse = new Course
                {
                    Id = oldCourse.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Content = request.Content,
                    Coverage = request.Coverage,
                    Subject = request.Subject
                };

                courseRepository.Update(updatedCourse);
                await courseRepository.SaveChanges();

                var courseDetails = GenericMapper<Course, CourseDetails>.Map(updatedCourse);

                return BaseResponse<CourseDetails>.Ok(courseDetails);
            }
            catch (Exception e)
            {
                return BaseResponse<CourseDetails>.BadRequest(new List<string>
                {
                    e.Message
                });
            }
        }
    }
}
