using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(
           PedagogyPrimeDbContext context
       ) : base(context)
        {
        }
    }
}
