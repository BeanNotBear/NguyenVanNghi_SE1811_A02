using DAL.Data;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
	public class UnitOfWork<TEnntity>(FunewsManagementSystemContext context) : IUnitOfWork<TEnntity> where TEnntity : class
	{
		private IGenericRepository<TEnntity>? _repository = null;
		public IGenericRepository<TEnntity> GenericRepository
		{
			get
			{
				if (_repository == null)
				{
					_repository = new GenericRepository<TEnntity>(context);
				}
				return _repository;
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			return await context.SaveChangesAsync();
		}
	}
}
