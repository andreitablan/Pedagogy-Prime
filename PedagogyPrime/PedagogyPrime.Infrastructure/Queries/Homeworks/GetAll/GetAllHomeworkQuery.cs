using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;
using PedagogyPrime.Infrastructure.Models.Homework;

namespace PedagogyPrime.Infrastructure.Queries.Homeworks.GetAll
{
    public class GetAllHomeworkQuery : BaseRequest<BaseResponse<List<HomeworkDetails>>>
    {
    }
}
