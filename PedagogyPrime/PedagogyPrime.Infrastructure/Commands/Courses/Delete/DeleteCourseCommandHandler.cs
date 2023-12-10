using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Delete
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class DeleteCourseCommandHandler : BaseRequestHandler<DeleteCourseCommand, BaseResponse<bool>>
	{
		private readonly ICourseRepository courseRepository;

		public DeleteCourseCommandHandler(
			ICourseRepository courseRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.courseRepository = courseRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteCourseCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

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