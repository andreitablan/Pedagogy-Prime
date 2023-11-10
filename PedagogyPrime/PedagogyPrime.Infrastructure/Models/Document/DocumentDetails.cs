using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Models.Document
{
    public class DocumentDetails
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public TypeDoc Type { get; set; } = TypeDoc.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
