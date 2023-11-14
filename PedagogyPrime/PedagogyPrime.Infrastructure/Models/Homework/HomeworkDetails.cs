
namespace PedagogyPrime.Infrastructure.Models.Homework
{
    using PedagogyPrime.Core.Entities;

    public class HomeworkDetails
    {
        public Grade Grade { get; set; }
        public string Review { get; set; }
        public string ContentUrl { get; set; }
        public Guid UserId { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
