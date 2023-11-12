namespace PedagogyPrime.Core.Entities
{
	public enum Role
	{
		Admin,
		Teacher,
		Student
	};

	public class User : BaseEntity
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public List<Document> Documents { get; set; }
		public List<UserSubject> UsersSubjects { get; set; }
	}
}