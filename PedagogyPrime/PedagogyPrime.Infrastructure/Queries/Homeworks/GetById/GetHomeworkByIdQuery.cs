using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.Homework;

namespace PedagogyPrime.Infrastructure.Queries.Homeworks.GetById
{
    public class GetHomeworkByIdQuery : BaseRequest<BaseResponse<HomeworkDetails>>
    {
       public Guid Id { get; set; }

		public GetHomeworkByIdQuery(Guid id)
		{
			Id = id;
		}
    }
}
