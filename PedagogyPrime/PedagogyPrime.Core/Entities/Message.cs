namespace PedagogyPrime.Core.Entities
{
	public class Message : BaseEntity
	{
		public string MessageText { get; set; }
		public DateTime Date { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
