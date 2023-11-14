namespace PedagogyPrime.Infrastructure.Commands.Homeworks.Update
{
	using Common;
	using Core.Common;
	using Models.User;
	using PedagogyPrime.Core.Entities;
	using PedagogyPrime.Infrastructure.Models.Homework;
	using System.Text.Json.Serialization;

	public class UpdateHomeworkCommand : BaseRequest<BaseResponse<HomeworkDetails>>
	{
		[JsonIgnore]
		public Guid Id { get; set; }

        public Grade Grade { get; set; }
        public string Review { get; set; }
        public string ContentUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid AssignmentId { get; set; }
    }
}