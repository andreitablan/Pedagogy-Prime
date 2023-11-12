using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class HomeworkRepository : BaseRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(
            PedagogyPrimeDbContext context
        ) : base(context)
        {
        }
    }
   
}
