using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Assignment;

namespace PedagogyPrime.Infrastructure.Queries.Assignments.GetById
{
    public class GetAssignmentByIdQuery : BaseRequest<BaseResponse<AssignmentDetails>>
    {
       public Guid Id { get; set; }

		public GetAssignmentByIdQuery(Guid id)
		{
			Id = id;
		}
    }
}
