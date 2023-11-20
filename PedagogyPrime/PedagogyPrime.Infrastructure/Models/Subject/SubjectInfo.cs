namespace PedagogyPrime.Infrastructure.Models.Subject
{
	using Course;

	public class SubjectInfo : SubjectDetails
	{
		public List<CourseDetails> CoursesDetails { get; set; }
	}
}