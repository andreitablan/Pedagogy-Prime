using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Create
{
    public class CreateCourseCommand : BaseRequest<BaseResponse<bool>>
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Content { get; set; }
        public Double Coverage { get; set; }
        public String Subject { get; set; }
    }
}
