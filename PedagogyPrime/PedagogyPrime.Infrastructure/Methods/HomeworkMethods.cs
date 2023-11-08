using PedagogyPrime.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyPrime.Infrastructure.Methods
{   //TODO (AT)
    //This is the homework entity
    public class HomeworkMethods
    {
        //CRUD methods
        public static Homework CreateHomework(Guid userId, string content, Grade grade, string review, string firebaseLink)
        {
            return null;
        }
        public static Homework GetHomework(Guid homeworkId)
        {
            return null;
        }

        public void UpdateHomework(string newContent, Grade newGrade, string newReview, string newFirebaseLink)
        {
        }

        public void DeleteHomework()
        {
        }

        public void SubmitSolution(string solutionLink)
        {
        }
    }
}
