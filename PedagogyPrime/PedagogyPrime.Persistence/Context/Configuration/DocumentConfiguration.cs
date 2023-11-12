using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Persistence.Context.Configuration
{
	internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
	{
		public void Configure(
			EntityTypeBuilder<Document> builder
		)
		{
			builder
				.HasOne(x => x.User)
				.WithMany(x => x.Documents)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
