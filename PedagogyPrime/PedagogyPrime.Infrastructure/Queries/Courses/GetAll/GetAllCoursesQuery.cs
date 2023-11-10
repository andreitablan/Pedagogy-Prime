using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Queries.Courses.GetAll
{
    public class GetAllCoursesQuery : BaseRequest<BaseResponse<List<CourseDetails>>>
    {
    }
}
