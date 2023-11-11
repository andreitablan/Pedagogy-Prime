namespace PedagogyPrime.Core.Entities
{
    public enum State
    {
        Approved, 
        Pending,
        Denied
    }
    public enum DocumentType
    {
        Public,
        Private
    }

    public class Document : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public DocumentType Type { get; set; } = DocumentType.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
