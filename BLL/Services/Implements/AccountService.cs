using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Shared.Enums;
using System.Linq.Expressions;

namespace BLL.Services.Implements
{
	public class AccountService(IUnitOfWork<SystemAccount> unitOfWork, IMapper mapper) : IAccountService
	{
		public async Task Create(CreateAccountDTO createAccountDTO)
		{
			var account = mapper.Map<SystemAccount>(createAccountDTO);
			await unitOfWork.GenericRepository.Insert(account);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<SystemAccountDTO>> GetAll(string? search = null, AccountRole? role = null)
		{
			Expression<Func<SystemAccount, bool>> predicate = x =>
			(
				(
					(string.IsNullOrWhiteSpace(search) || x.AccountName.ToLower() == search.ToLower()) ||
					(string.IsNullOrWhiteSpace(search)) || x.AccountEmail.ToLower() == search.ToLower()
				) && (role.HasValue == false || x.AccountRole == (int)(role.Value))
			);
			return mapper.Map<IEnumerable<SystemAccountDTO>>(await unitOfWork.GenericRepository.GetAll(predicate));
		}
	}
}
