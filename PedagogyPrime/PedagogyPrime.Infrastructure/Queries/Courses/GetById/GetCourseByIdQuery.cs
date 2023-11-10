using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetById
{
    public class GetCourseByIdQuery : BaseRequest<BaseResponse<CourseDetails>>
    {
        public Guid Id { get; set; }
    }
}
