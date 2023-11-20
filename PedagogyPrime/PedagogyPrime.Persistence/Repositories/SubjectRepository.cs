using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
	using Microsoft.EntityFrameworkCore;

	public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
	{
		public SubjectRepository(
		   PedagogyPrimeDbContext context
	   ) : base(context)
		{
		}

		public override async Task<Subject?> GetById(
			Guid id
		)
		{
			return await _context.Subjects.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}