namespace PedagogyPrime.Core.Entities
{
	public class CourseMessage : Message
	{
		public Guid CourseForumId { get; set; }
		public CourseForum CourseForum { get; set; }
	}
}