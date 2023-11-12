using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyPrime.Infrastructure.Queries.Assignments.GetAll
{
    public class GetAssignmentsByIdQuery : BaseRequest<BaseResponse<AssignmentDetails>>
    {
        public Guid Id { get; set; }
    }
}
