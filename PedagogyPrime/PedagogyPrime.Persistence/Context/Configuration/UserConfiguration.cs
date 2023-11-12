using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PedagogyPrime.Persistence.Context.Configuration
{
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;

	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(
			EntityTypeBuilder<User> builder
		)
		{
			builder.Property(x => x.Role).IsRequired();
			builder.Property(x => x.Password).IsRequired();
			builder.Property(x => x.FirstName).IsRequired();
			builder.Property(x => x.LastName).IsRequired();
			builder.Property(x => x.Email).IsRequired();

			//builder.HasMany(x => x.Documents).WithOne(x => x.User);
		}
	}
}