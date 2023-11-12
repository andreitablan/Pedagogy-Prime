using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.Homework;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Infrastructure.Queries.Assignments.GetById;
using PedagogyPrime.Infrastructure.Queries.Users.GetById;
using PedagogyPrime.Persistence.Repositories;

namespace PedagogyPrime.Infrastructure.Queries.Homeworks.GetById
{
    public class GetHomeworkByIdQueryHandler : BaseRequestHandler<GetHomeworkByIdQuery, BaseResponse<HomeworkDetails>>
    {
        private readonly IHomeworkRepository homeworkRepository;

        public GetHomeworkByIdQueryHandler(IHomeworkRepository homeworkRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
        {
            this.homeworkRepository = homeworkRepository;
        }
        public override async Task<BaseResponse<HomeworkDetails>> Handle(
            GetHomeworkByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var homework = await homeworkRepository.GetById(request.Id);

                if (homework == null)
                {
                    return BaseResponse<HomeworkDetails>.NotFound("Homework");
                }

                if (!(await IsAuthorized(request.UserId, homework.Id)))
                {
                    return BaseResponse<HomeworkDetails>.Forbbiden();
                }

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
