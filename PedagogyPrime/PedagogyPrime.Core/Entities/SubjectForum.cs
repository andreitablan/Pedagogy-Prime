namespace PedagogyPrime.Core.Entities
{
	public class SubjectForum : BaseEntity
	{
		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }
		public List<SubjectMessage> SubjectMessages { get; set; }
	}
}