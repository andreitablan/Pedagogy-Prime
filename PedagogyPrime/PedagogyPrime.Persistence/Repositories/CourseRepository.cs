using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
	using Microsoft.EntityFrameworkCore;

	public class CourseRepository : BaseRepository<Course>, ICourseRepository
	{
		public CourseRepository(
		   PedagogyPrimeDbContext context
	   ) : base(context)
		{
		}

		public override async Task<List<Course>> GetAll()
		{
			return await _context.Courses.Include(x => x.Coverage).ToListAsync();
		}

		public override async Task<Course?> GetById(
			Guid id
		)
		{
			return await _context.Courses.Include(x => x.Coverage).FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}