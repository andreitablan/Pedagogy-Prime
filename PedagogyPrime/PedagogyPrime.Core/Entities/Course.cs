namespace PedagogyPrime.Core.Entities
{
	public class Course : BaseEntity
	{
		public String Name { get; set; }
		public String Description { get; set; }
		public String Content { get; set; }
		public Double Coverage { get; set; }
		public String Subject { get; set; }
	}
}