namespace PedagogyPrime.Persistence.Repositories
{
	using Context;
	using Core.Entities;
	using Core.IRepositories;
	using Microsoft.EntityFrameworkCore;

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

		public virtual async Task<List<TEntity>> GetAll()
		{
			return await _context
				.Set<TEntity>()
				.AsNoTracking()
				.ToListAsync();
		}

		public virtual async Task<TEntity?> GetById(
			Guid id
		)
		{
			return await _context
				.Set<TEntity>()
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task Add(
			TEntity entity
		)
		{
			await _context
				.Set<TEntity>()
				.AddAsync(entity);
		}

		public void Update(
			TEntity entity
		)
		{
			_context
				.Set<TEntity>()
				.Update(entity);
		}

		public virtual async Task Delete(
			Guid id
		)
		{
			var entity = await GetById(id);

			if(entity == null)
			{
				throw new Exception("No entity found with specified id");
			}

			_context
				.Set<TEntity>()
				.Remove(entity);
		}

		public async Task<int> SaveChanges()
		{
			return await _context.SaveChangesAsync();
		}
	}
}