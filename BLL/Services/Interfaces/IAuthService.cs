using DAL.Entities;

namespace BLL.Services.Interfaces
{
	public interface IAuthService
	{
		Task<SystemAccount?> Login(string email, string password);
	}
}
