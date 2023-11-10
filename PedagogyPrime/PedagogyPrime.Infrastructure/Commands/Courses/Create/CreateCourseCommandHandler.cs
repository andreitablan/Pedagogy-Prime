using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, BaseResponse<bool>>
    {
        private readonly ICourseRepository courseRepository;

        public CreateCourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<BaseResponse<bool>> Handle(
            CreateCourseCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var course = new Course
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Content = request.Content,
                    Coverage = request.Coverage,
                    Subject = request.Subject
                };
                await courseRepository.Add(course);
                await courseRepository.SaveChanges();

                return BaseResponse<bool>.Created();
            }
            catch (Exception e)
            {
                return BaseResponse<bool>.BadRequest(new List<string>
                {
                    e.Message
                });
            }
        }
    }
}
