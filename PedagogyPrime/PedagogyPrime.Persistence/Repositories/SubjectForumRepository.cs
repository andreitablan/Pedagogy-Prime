using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
	using Microsoft.EntityFrameworkCore;

	public class SubjectForumRepository : BaseRepository<SubjectForum>, ISubjectForumRepository
	{
		public SubjectForumRepository(
		   PedagogyPrimeDbContext context
	   ) : base(context)
		{
		}

		public async Task<SubjectForum> GetBySubjectId(
			Guid subjectId
		)
		{
			return await _context.SubjectForums.AsNoTracking().FirstOrDefaultAsync(x => x.SubjectId == subjectId);
		}
	}
}