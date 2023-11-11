using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Persistence.Context;

namespace PedagogyPrime.Persistence.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(
            PedagogyPrimeDbContext context
        ) : base(context)
        {
        }
    }
}
