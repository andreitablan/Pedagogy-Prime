namespace PedagogyPrime.Infrastructure.Commands.SubjectForums.Update
{
	using Common;
	using Core.Common;
	using Models.SubjectForum;
	using PedagogyPrime.Core.Entities;
	using System.Text.Json.Serialization;

	public class UpdateSubjectForumCommand : BaseRequest<BaseResponse<SubjectForumDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public Guid SubjectId { get; set; }
	}
}