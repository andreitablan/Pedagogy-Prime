using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Create
{
    public class CreateSubjectCommand : BaseRequest<BaseResponse<Guid>>
    {
        public string Name { get; set; } = String.Empty;
        public string Period { get; set; } = String.Empty;
        public int NoOfCourses { get; set; }
    }
}
