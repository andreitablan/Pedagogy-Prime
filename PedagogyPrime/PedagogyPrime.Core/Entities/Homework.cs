using System;

namespace PedagogyPrime.Core.Entities
{
    public enum Grade
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten
    };
    public class Homework : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public Grade Grade { get; set; }
        public string Review { get; set; }
        public string FirebaseLink { get; set; }
    }
}