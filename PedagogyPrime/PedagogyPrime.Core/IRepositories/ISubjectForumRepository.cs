namespace PedagogyPrime.Core.IRepositories
{
	using PedagogyPrime.Core.Entities;

	public interface ISubjectForumRepository : IBaseRepository<SubjectForum>
	{
		Task<SubjectForum> GetBySubjectId(
			Guid subjectId
		);
	}
}