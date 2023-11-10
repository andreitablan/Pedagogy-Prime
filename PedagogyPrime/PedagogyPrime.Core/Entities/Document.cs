﻿namespace PedagogyPrime.Core.Entities
{
    public enum State
    {
        Approved, 
        Pending,
        Denied
    }
    public enum TypeDoc
    {
        Public,
        Private
    }

    public class Document : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = String.Empty;
        public State State { get; set; }
        public TypeDoc Type { get; set; } = TypeDoc.Public;
        public String FirebaseLink { get; set; } = String.Empty;
    }
}
