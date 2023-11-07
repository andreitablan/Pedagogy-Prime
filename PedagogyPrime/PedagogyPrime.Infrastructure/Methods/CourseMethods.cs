using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Methods
{
    //TODO (OR)
    //This is the course of a teacher related to his subject
    public class CourseMethods
    {
        //CRUD
        //get the list of the courses for that subject based of the idSubject. It will be a common place for all teachers
        public List<Course> GetCourses(Guid idSubject) {return null;}
        public Course AddCourse(Guid idSubject, Guid idTeacher) { return null; }
        public void DeleteCourse(Guid idSubject, Guid idTeacher) {}
        public Course UpdateCourse(Guid idSubject, Guid idTeacher) { return null; }

    }
}
