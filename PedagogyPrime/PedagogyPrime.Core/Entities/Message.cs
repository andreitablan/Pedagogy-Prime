namespace PedagogyPrime.Core.Entities
{
    public class Message : BaseEntity
    {
        public Guid UserId { get; set; }
        public string MessageText {  get; set; }
        public DateTime Date {  get; set; }
    }
}
