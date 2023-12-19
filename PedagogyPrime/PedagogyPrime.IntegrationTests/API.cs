using System.Reflection;

namespace PedagogyPrime.IntegrationTests
{
    public static class API
    {
        private const string BaseUrl = "https://localhost:7136/api";
        public static class Assignments
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Assignments";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Assignments/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Assignments";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Assignments";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Assignments";
        }
        public static class Authentication
        {
            public static string login(string version) => $"{BaseUrl}/{version}/Authentication/login";
        }
        public static class Courses
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Courses";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Courses/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Courses";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Courses/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Courses/{id}";
        }
        public static class Coverage
        {
            public static string Post(string version) => $"{BaseUrl}/{version}/Coverage";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Coverage/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Coverage/{id}";
        }
        public static class Documents
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Documents";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Documents/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Documents";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Documents/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Documents/{id}";
        }
        public static class Homeworks
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Homeworks";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Homeworks/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Homeworks";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Homeworks/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Homeworks/{id}";
        }
        public static class SubjectForum
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/SubjectForum";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/SubjectForum/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/SubjectForum";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/SubjectForum/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/SubjectForum/{id}";
        }
        public static class Subjects
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Subjects";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Subjects/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Subjects";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Subjects/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Subjects/{id}";
        }
        public static class Users
        {
            public static string GetAll(string version) => $"{BaseUrl}/{version}/Users";
            public static string GetById(string version, Guid id) => $"{BaseUrl}/{version}/Users/{id}";
            public static string Post(string version) => $"{BaseUrl}/{version}/Users";
            public static string Put(string version, Guid id) => $"{BaseUrl}/{version}/Users/{id}";
            public static string Delete(string version, Guid id) => $"{BaseUrl}/{version}/Users/{id}";
            public static string GetUsersBySubjectId(string version, Guid id) => $"{BaseUrl}/{version}/Users/{id}/subjects";
            public static string GetUsersNotRegisteredAtSubjectBySubjectId(string version, Guid id) => $"{BaseUrl}/{version}/Users/notRegisteredAtSubject/{id}";
        }
    }
}
