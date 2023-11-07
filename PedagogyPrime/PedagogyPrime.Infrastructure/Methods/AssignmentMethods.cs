using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyPrime.Infrastructure.Methods
{
    //TODO(TA)
    //This is the assignment given by a teacher on a subject
    public class AssignmentMethods
    {
        //CRUD methods
        public List<Assignment> GetAssignments(Guid courseId)
        {
            return null;
        }

        public Assignment AddAssignment(Guid courseId, DateTime deadline, string description)
        {
            return null;
        }

        public void DeleteAssignment(Guid assignmentId)
        {
        }

        public Assignment UpdateAssignment(Guid assignmentId, DateTime newDeadline, string newDescription)
        {
            return null;
        }
    }
}
