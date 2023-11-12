namespace PedagogyPrime.Core.Entities
{
	public class CourseForum : BaseEntity
	{
		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}