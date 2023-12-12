namespace PedagogyPrime.Infrastructure.Models.Coverage
{
	public class CoverageDetails
	{
		public Guid Id { get; set; }
		public Double Percentage { get; set; }
		public List<string> GoodWords { get; set; }
		public List<string> BadWords { get; set; }
    }
}