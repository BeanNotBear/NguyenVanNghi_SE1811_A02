using BLL.MapperProfiles;
using BLL.Services.Implements;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
	public static class BLLServices
	{
		public static void AddBLLServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(BLLServices));
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<INewsArticleService, NewsArticleService>();
			services.AddScoped<ITagService, TagSerive>();
		}
	}
}
