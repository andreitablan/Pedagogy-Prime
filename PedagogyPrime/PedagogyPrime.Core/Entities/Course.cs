namespace PedagogyPrime.Core.Entities
{
	public class Course : BaseEntity
	{
		public String Name { get; set; }
		public String Description { get; set; }
		public String ContentUrl { get; set; }
		public Double Coverage { get; set; }
		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }
		public CourseForum CourseForum { get; set; }
	}
}