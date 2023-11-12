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
		public DbSet<CourseForum> CourseForums => Set<CourseForum>();
		public DbSet<CourseMessage> CourseMessages => Set<CourseMessage>();
		public DbSet<Document> Documents => Set<Document>();
		public DbSet<Assignment> Assignments => Set<Assignment>();
		public DbSet<Homework> Homework => Set<Homework>();
		public DbSet<Subject> Subjects => Set<Subject>();
		public DbSet<SubjectForum> SubjectForums => Set<SubjectForum>();
		public DbSet<SubjectMessage> SubjectMessages => Set<SubjectMessage>();
		public DbSet<UserSubject> UsersSubjects => Set<UserSubject>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}