using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Homework;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Queries.Homeworks.GetAll
{
    public class GetAllHomeworkQueryHandler : BaseRequestHandler<GetAllHomeworkQuery, BaseResponse<List<HomeworkDetails>>>
    {
        private readonly IHomeworkRepository homeworkRepository;

        public GetAllHomeworkQueryHandler(IHomeworkRepository homeworkRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
        {
            this.homeworkRepository = homeworkRepository;
        }
        public override async Task<BaseResponse<List<HomeworkDetails>>> Handle(
            GetAllHomeworkQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var homework = await homeworkRepository.GetAll();

                var homeworkDetails = homework.Select(GenericMapper<Homework, HomeworkDetails>.Map).ToList();

                return BaseResponse<List<HomeworkDetails>>.Ok(homeworkDetails);
            }
            catch
            {
                return BaseResponse<List<HomeworkDetails>>.InternalServerError();
            }
        }
    }
}
