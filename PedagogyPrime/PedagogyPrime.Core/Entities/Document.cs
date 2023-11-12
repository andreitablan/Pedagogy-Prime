namespace PedagogyPrime.Core.Entities
{
	public enum State
	{
		Approved,
		Pending,
		Denied
	}
	public enum DocumentType
	{
		Public,
		Private
	}

	public class Document : BaseEntity
	{
		public State State { get; set; }
		public DocumentType Type { get; set; }
		public String ContentUrl { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
