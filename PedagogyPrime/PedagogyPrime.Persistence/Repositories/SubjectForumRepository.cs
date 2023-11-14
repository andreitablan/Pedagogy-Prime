using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class SubjectForumRepository : BaseRepository<SubjectForum>, ISubjectForumRepository
    {
        public SubjectForumRepository(
           PedagogyPrimeDbContext context
       ) : base(context)
        {
        }
    }
}
