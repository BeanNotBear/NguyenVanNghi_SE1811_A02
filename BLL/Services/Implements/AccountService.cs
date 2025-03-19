using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Shared.Enums;
using Shared.PasswordHasher;
using System.Data;
using System.Linq.Expressions;

namespace BLL.Services.Implements
{
	public class AccountService(IUnitOfWork<SystemAccount> unitOfWork, IMapper mapper) : IAccountService
	{
		public async Task Create(CreateAccountDTO createAccountDTO)
		{
			var account = mapper.Map<SystemAccount>(createAccountDTO);
			account.AccountPassword = PasswordHasher.Instance.Hash(account.AccountPassword);
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
			var accounts = mapper.Map<IEnumerable<SystemAccountDTO>>(await unitOfWork.GenericRepository.GetAll(predicate));
			return accounts;
		}

		public async Task<SystemAccountDetailDTO> GetByID(int ID, string? properties = null)
		{
			Expression<Func<SystemAccount, bool>> predicate = x =>
			(
				x.AccountId == ID
			);
			string[]? joins = null;
			if (properties != null)
			{
				joins = properties.Split(',');
			}
			var account = await unitOfWork.GenericRepository.Get(predicate, joins);
			return mapper.Map<SystemAccountDetailDTO>(account);
		}

		public async Task Update(EditAccountDTO editAccountDTO)
		{
			var account = mapper.Map<SystemAccount>(editAccountDTO);
			account.AccountPassword = PasswordHasher.Instance.Hash(account.AccountPassword);
			unitOfWork.GenericRepository.Update(account);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
