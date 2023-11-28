using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Core.IRepositories
{
    public interface IUserSubjectRepository : IBaseRepository<UserSubject>
    {
        Task<List<User>> GetAllUsersBySubjectId(Guid subjectId);
    }
}
