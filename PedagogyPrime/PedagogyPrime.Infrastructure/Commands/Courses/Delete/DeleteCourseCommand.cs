using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using System.Text.Json.Serialization;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Delete
{
    public class DeleteCourseCommand : BaseRequest<BaseResponse<bool>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
