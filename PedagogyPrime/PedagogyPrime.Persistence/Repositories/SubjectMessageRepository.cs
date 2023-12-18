namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Microsoft.EntityFrameworkCore;
	using PedagogyPrime.Core.IRepositories;

	public class SubjectMessageRepository : BaseRepository<SubjectMessage>, ISubjectMessageRepository
	{
		public SubjectMessageRepository(
			PedagogyPrimeDbContext context
		) : base(context)
		{
		}

		public async Task<List<SubjectMessage>> GetAllBySubjectForumId(
			Guid subjectForumId
		)
		{
			return await _context.SubjectMessages.Include(x => x.User).Where(x => x.SubjectForumId == subjectForumId).ToListAsync();
		}
	}
}