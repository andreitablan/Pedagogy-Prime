using Microsoft.EntityFrameworkCore;
using PedagogyPrime.Core.Entities;
using System.Reflection;

namespace PedagogyPrime.Persistence.Context
{
	public class PedagogyPrimeDbContext : DbContext
	{
		public PedagogyPrimeDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users => Set<User>();
		public DbSet<Course> Courses => Set<Course>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}