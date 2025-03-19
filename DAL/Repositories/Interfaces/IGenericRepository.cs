using System.Linq.Expressions;

namespace DAL.Repositories.Interfaces
{
	public interface IGenericRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? predicates = null, Func<IReadOnlyList<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? properties = null);
		Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicates, string[]? properties = null);
		Task Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}
