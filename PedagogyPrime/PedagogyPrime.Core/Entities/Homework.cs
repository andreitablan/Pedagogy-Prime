namespace PedagogyPrime.Core.Entities
{
	public enum Grade
	{
		One,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Solution
	};
	public class Homework : BaseEntity
	{
		public Grade? Grade { get; set; }
		public string? Review { get; set; }
		public string ContentUrl { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid AssignmentId { get; set; }
		public Assignment Assignment { get; set; }
	}
}