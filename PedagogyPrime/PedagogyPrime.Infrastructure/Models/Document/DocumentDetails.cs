using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Models.Document
{
    public class DocumentDetails
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public DocumentType Type { get; set; } = DocumentType.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
