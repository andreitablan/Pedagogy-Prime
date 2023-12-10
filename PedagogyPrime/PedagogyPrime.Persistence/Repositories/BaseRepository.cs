namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Core.IRepositories;
	using Microsoft.EntityFrameworkCore;
	using PedagogyPrime.Persistence.AOP;

	public class BaseRepository<TEntity> : IBaseRepository<TEntity>
		where TEntity : BaseEntity
	{
		protected readonly PedagogyPrimeDbContext _context;

		protected BaseRepository(
			PedagogyPrimeDbContext context
		)
		{
			_context = context;
			_context.ChangeTracker.Clear();
		}
		[TraceRepositoryAspect]
		public virtual async Task<List<TEntity>> GetAll()
		{
			return await _context
				.Set<TEntity>()
				.AsNoTracking()
				.ToListAsync();
		}
		[TraceRepositoryAspect]
		public virtual async Task<TEntity?> GetById(
			Guid id
		)
		{
			return await _context
				.Set<TEntity>()
				.FirstOrDefaultAsync(e => e.Id == id);
		}
		[TraceRepositoryAspect]
		public async Task Add(
			TEntity entity
		)
		{
			await _context
				.Set<TEntity>()
				.AddAsync(entity);
		}
		[TraceRepositoryAspect]
		public void Update(
			TEntity entity
		)
		{
			_context
				.Set<TEntity>()
				.Update(entity);
		}
		[TraceRepositoryAspect]
		public virtual async Task<int> Delete(
			Guid id
		)
		{
			return await _context
				.Set<TEntity>()
				.DeleteByKeyAsync(id);
		}
		[TraceRepositoryAspect]
		public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}
	}
}