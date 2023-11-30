namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class CoverageConfiguration : IEntityTypeConfiguration<Coverage>
	{
		public void Configure(
			EntityTypeBuilder<Coverage> builder
		)
		{
			builder
				.Property(e => e.BadWords)
				.HasConversion(
					v => string.Join(",", v),
					v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

			builder
				.Property(e => e.GoodWords)
				.HasConversion(
					v => string.Join(",", v),
					v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
		}
	}
}