namespace PedagogyPrime.Infrastructure.Commands.Subjects.Update
{
	using Common;
	using Core.Common;
	using Models.Subject;
	using PedagogyPrime.Core.Entities;
	using System.Text.Json.Serialization;

	public class UpdateSubjectCommand : BaseRequest<BaseResponse<SubjectDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Period { get; set; }
		public int NoOfCourses { get; set; }
	}
}