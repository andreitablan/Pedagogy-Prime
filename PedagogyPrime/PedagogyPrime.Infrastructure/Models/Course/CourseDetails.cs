namespace PedagogyPrime.Infrastructure.Models.Course
{
	using Coverage;

	public class CourseDetails
	{
		public Guid Id { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
		public String ContentUrl { get; set; }
		public CoverageDetails Coverage { get; set; }
		public int Index { get; set; }
		public bool IsVisibleForStudents { get; set; }
	}
}