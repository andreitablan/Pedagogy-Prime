using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(
           PedagogyPrimeDbContext context
       ) : base(context)
        {
        }
    }
}
