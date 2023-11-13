
namespace PedagogyPrime.Infrastructure.Models.Assignment
{
    using PedagogyPrime.Core.Entities;
    public class AssignmentDetails
    {
            public DateTime Deadline { get; set; }
            public string Description { get; set; }
            public List<Homework> Homeworks { get; set; }
            public Guid SolutionId { get; set; }
            public Guid CourseId { get; set; }
            public Course Course { get; set; }
    }
}
