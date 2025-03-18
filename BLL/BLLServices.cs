using BLL.Services.Implements;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
	public static class BLLServices
	{
		public static void AddBLLServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthService>();
		}
	}
}
