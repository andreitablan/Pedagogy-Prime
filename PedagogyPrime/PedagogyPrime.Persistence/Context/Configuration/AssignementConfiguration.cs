using Microsoft.EntityFrameworkCore;
using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class AssignementConfiguration : IEntityTypeConfiguration<Assignment>
	{
		public void Configure(
			EntityTypeBuilder<Assignment> builder
		)
		{
		}
	}
}