namespace PedagogyPrime.Core.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string Period {  get; set; }
        public int NoOfCourses {  get; set; }
        public List<Course> Courses { get; set; }
        public List<User> Users { get; set; }
        public Forum Forum { get; set; }
    }
}
