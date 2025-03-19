using DAL.Data;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories.Implements
{
	public class GenericRepository<TEntity>(FunewsManagementSystemContext context) : IGenericRepository<TEntity>, IDisposable where TEntity : class
	{
		public void Delete(TEntity entity)
		{
			context.Set<TEntity>().Remove(entity);
		}

		public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? predicates = null,
			Func<IReadOnlyList<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? properties = null)
		{
			var query = context.Set<TEntity>().AsQueryable();
			if (predicates != null)
			{
				query = query.Where(predicates);
			}
			if (orderBy != null)
			{
				query = orderBy(await query.ToListAsync());
			}
			if(properties != null)
			{
				foreach (var item in properties)
				{
					query = query.Include(item);
				}
			}
			return await query.ToListAsync();
		}

		public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicates, string[]? properties = null)
		{
			var query = context.Set<TEntity>().AsQueryable();
			if (properties != null)
			{
				foreach (var property in properties)
				{
					query = query.Include(property.Trim());
				}
			}
			return await query.FirstOrDefaultAsync(predicates);
		}

		public async Task Insert(TEntity entity)
		{
			await context.Set<TEntity>().AddAsync(entity);
		}

		public void Update(TEntity entity)
		{
			context.Set<TEntity>().Update(entity);
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
