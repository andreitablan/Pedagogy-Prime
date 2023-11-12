namespace PedagogyPrime.Core.Entities
{
	public class SubjectMessage : Message
	{
		public Guid SubjectForumId { get; set; }
		public SubjectForum SubjectForum { get; set; }
	}
}