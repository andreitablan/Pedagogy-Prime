namespace PedagogyPrime.Core.Entities
{
	public class Coverage : BaseEntity
		public Double Percentage { get; set; }
		public List<string> GoodWords { get; set; }
		public List<string> BadWords { get; set; }
		public Guid CourseId { get; set; }
		public Course Course { get; set; }
	}
}