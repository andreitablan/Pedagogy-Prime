namespace PedagogyPrime.Infrastructure.Models.Course
{
    public class CourseDetails
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ContentUrl { get; set; }
        public Double Coverage { get; set; }
        public int Index { get; set; }
    }
}