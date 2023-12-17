namespace PedagogyPrime.Infrastructure.Models.Message
{
	public class MessageDetails
	{
		public string MessageText { get; set; }
		public string Username { get; set; }
		public Guid UserId { get; set; }
		public DateTime Date { get; set; }
	}
}