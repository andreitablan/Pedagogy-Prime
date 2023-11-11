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
				var result = await courseRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Course");
				}

				return BaseResponse<bool>.Ok(true);
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}