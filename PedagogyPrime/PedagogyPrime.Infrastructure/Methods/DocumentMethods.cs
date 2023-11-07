using PedagogyPrime.Core.Entities;

namespace PedagogyPrime.Infrastructure.Methods
{
    //TODO (OR)
    //We need to connect to a databse which is designed to store pdf/doc files
    public class DocumentMethods
    {
        //CRUD
        //Each course has a set of documents which where parsed from our analyzer and can be seen by students and teacher
        public List<Document> GetDocuments(Guid idCourse) { return null; }
        public Document AddDocument(Guid idCourse, Guid idTeacher) { return null; }
        public void DeleteDocument(Guid idCourse, Guid idTeacher) {}
        public Document UpdateDocument(Guid idCourse, Guid idTeacher) { return null; }
    }
}
