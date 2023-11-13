namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Create
{
	using Common;
	using Core.Entities;
	using Core.IRepositories;
	using PedagogyPrime.Core.Common;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class CreateHomeworkCommandHandler : BaseRequestHandler<CreateHomeworkCommand, BaseResponse<bool>>
	{
		private readonly IHomeworkRepository homeworkRepository;

		public CreateHomeworkCommandHandler(
            IHomeworkRepository homeworkRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.homeworkRepository = homeworkRepository;
		}

		public override async Task<BaseResponse<bool>> Handle(
            CreateHomeworkCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var homework = new Homework
				{
					Id = Guid.NewGuid(),
                    Review = request.Review,
					ContentUrl = request.ContentUrl,
					UserId = request.UserId,
					AssignmentId = request.AssignmentId,
                };

				await homeworkRepository.Add(homework);
				await homeworkRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}