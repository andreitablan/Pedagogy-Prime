using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Models.Subject
{
    public class SubjectDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public int NoOfCourses { get; set; }
    }
}
