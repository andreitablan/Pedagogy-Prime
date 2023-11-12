namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>

	{
		public void Configure(
			EntityTypeBuilder<Homework> builder
		)
		{
			builder.Property(x => x.ContentUrl).IsRequired();
		}
	}
}