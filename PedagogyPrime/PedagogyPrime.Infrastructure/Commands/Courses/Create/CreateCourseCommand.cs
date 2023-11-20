using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Create
{
	public class CreateCourseCommand : BaseRequest<BaseResponse<Guid>>
	{
		public String Name { get; set; }
		public String Description { get; set; }
		public String ContentUrl { get; set; }
		public Double Coverage { get; set; }
		public Guid SubjectId { get; set; }
		public int Index { get; set; }
	}
}