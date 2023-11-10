using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Delete
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, BaseResponse<bool>>
    {
        private readonly ICourseRepository courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<BaseResponse<bool>> Handle(
            DeleteCourseCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var course = await courseRepository.GetById(request.UserId);
                if (course == null)
                {
                    return BaseResponse<bool>.NotFound();
                }

                await courseRepository.Delete(request.UserId);
                await courseRepository.SaveChanges();
                return BaseResponse<bool>.Ok(); 
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
