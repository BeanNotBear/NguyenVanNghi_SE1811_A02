using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Shared.Enums;
using Shared.PasswordHasher;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BLL.Services.Implements
{
	public class AuthService(IUnitOfWork<SystemAccount> unitOfWork) : IAuthService
	{
		public async Task<SystemAccount?> Login(string email, string password)
		{
			Expression<Func<SystemAccount, bool>> predicate = x => (
				x.AccountEmail.ToLower() == email.ToLower()
			);
			var user = await unitOfWork.GenericRepository.Get(predicate);
			var isCorrect = PasswordHasher.Instance.Verify(password, user.AccountPassword);
			return isCorrect ? user : null;
		}
	}
}
