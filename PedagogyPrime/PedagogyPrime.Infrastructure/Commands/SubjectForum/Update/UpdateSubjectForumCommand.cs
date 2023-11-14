namespace PedagogyPrime.Infrastructure.Commands.SubjectForum.Update
{
	using Common;
	using Core.Common;
	using Models.User;
	using PedagogyPrime.Core.Entities;
	using System.Text.Json.Serialization;

	public class UpdateSubjectForumCommand : BaseRequest<BaseResponse<SubjectForumDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }
		public Guid SubjectId { get; set; }
	}
}