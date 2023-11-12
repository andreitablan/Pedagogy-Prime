namespace PedagogyPrime.Persistence.Entities.Configuration
{
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class CourseConfiguration : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder
				.HasOne(x => x.Subject)
				.WithMany(x => x.Courses)
				.HasForeignKey(b => b.SubjectId)  // Foreign key property
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
