using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(
           PedagogyPrimeDbContext context
       ) : base(context)
        {
        }
    }
}
