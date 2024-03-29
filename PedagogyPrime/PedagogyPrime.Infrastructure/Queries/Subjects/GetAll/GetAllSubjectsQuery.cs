﻿using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Subject;

namespace PedagogyPrime.Infrastructure.Queries.Subjects.GetAll
{
    public class GetAllSubjectsQuery : BaseRequest<BaseResponse<List<SubjectDetails>>>
    {
    }
}
