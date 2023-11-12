namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class UserSubjectConfiguration : IEntityTypeConfiguration<UserSubject>
	{
		public void Configure(
			EntityTypeBuilder<UserSubject> builder
		)
		{
			builder
				.HasKey(x => new { x.UserId, x.SubjectId });

			builder
				.HasOne(x => x.Subject)
				.WithMany(x => x.UsersSubjects)
				.HasForeignKey(x => x.SubjectId);

			builder
				.HasOne(x => x.User)
				.WithMany(x => x.UsersSubjects)
				.HasForeignKey(x => x.UserId);
		}
	}
}