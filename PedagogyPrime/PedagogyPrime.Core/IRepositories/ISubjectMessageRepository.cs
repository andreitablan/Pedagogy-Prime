namespace PedagogyPrime.Core.IRepositories
{
	using Entities;

	public interface ISubjectMessageRepository : IBaseRepository<SubjectMessage>
	{
		Task<List<SubjectMessage>> GetAllBySubjectForumId(
			Guid subjectForumId
		);
	}
}