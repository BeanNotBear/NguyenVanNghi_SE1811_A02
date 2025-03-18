namespace DAL.Repositories.Interfaces
{
	public interface IUnitOfWork<TEntity> where TEntity : class
	{
		public IGenericRepository<TEntity> GenericRepository { get; }

		Task<int> SaveChangesAsync();
	}
}
