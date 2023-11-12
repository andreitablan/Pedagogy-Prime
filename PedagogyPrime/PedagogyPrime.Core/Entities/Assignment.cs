namespace PedagogyPrime.Core.Entities
{
	public class Assignment : BaseEntity
	{
		public DateTime Deadline { get; set; }
		public string Description { get; set; }
		public List<Homework> Homeworks { get; set; }
		public Guid SolutionId { get; set; }
		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}