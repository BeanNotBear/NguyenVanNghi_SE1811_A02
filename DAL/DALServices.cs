using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
	public static class DALServices
	{

		public static void AddDALServives(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		}
	}
}
