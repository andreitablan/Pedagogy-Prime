namespace PedagogyPrime.Infrastructure.Commands.Users.Update
{
	using Common;
	using Core.Common;
	using Core.Entities;
	using Core.IRepositories;
	using IAuthorization;
	using Models.User;
	using PedagogyPrime.Infrastructure.Commands.Homeworks.Update;
	using PedagogyPrime.Infrastructure.Models.Homework;

	public class UpdateHomeworkCommandHandler : BaseRequestHandler<UpdateHomeworkCommand, BaseResponse<HomeworkDetails>>
	{
		private readonly IHomeworkRepository homeworkRepository;

		public UpdateHomeworkCommandHandler(
            IHomeworkRepository homeworkRepository,
			IUserAuthorization userAuthorization
		) : base(userAuthorization)
		{
			this.homeworkRepository = homeworkRepository;
		}

		public override async Task<BaseResponse<HomeworkDetails>> Handle(
            UpdateHomeworkCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<HomeworkDetails>.Forbbiden();
				}

				var homework = await homeworkRepository.GetById(request.Id);

				if(homework == null)
				{
					return BaseResponse<HomeworkDetails>.NotFound("Homework");
				}

				homework.Grade = request.Grade;
				homework.Review = request.Review;
				homework.ContentUrl = request.ContentUrl;
				homework.UserId = request.UserId;
				homework.AssignmentId = request.AssignmentId;


                await homeworkRepository.SaveChanges();

				var homeworkDetails = GenericMapper<Homework, HomeworkDetails>.Map(homework);

				return BaseResponse<HomeworkDetails>.Ok(homeworkDetails);
			}
			catch
			{
				return BaseResponse<HomeworkDetails>.InternalServerError();
			}
		}
	}
}