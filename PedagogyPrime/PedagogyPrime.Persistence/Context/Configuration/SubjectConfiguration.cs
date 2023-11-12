using Microsoft.EntityFrameworkCore;
using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
	{
		public void Configure(
			EntityTypeBuilder<Subject> builder
		)
		{
			builder.Property(x => x.Name).IsRequired();
			builder.Property(x => x.NoOfCourses).IsRequired();
			builder.Property(x => x.Period).IsRequired();

			//builder.HasOne(x => x.SubjectForum).WithOne(x => x.Subject).HasForeignKey<SubjectForum>(x => x.SubjectId);
		}
	}
}