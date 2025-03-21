using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Shared.Enums;
using Shared.PasswordHasher;
using System;
using System.Data;
using System.Drawing;
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
			return mapper.Map<SystemAccountDetailDTO>(await FindByID(ID, properties));
		}

		public async Task Update(EditAccountDTO editAccountDTO)
		{

			var account = await FindByID(editAccountDTO.AccountID);
			account.AccountName = editAccountDTO.AccountName;
			account.AccountEmail = editAccountDTO.AccountEmail;
			account.AccountRole = (int)(editAccountDTO.AccountRole);
			unitOfWork.GenericRepository.Update(account);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var account = await FindByID(id);
			unitOfWork.GenericRepository.Delete(account);
			await unitOfWork.SaveChangesAsync();
		}

		private async Task<SystemAccount?> FindByID(int ID, string? properties = null)
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
			return account;
		}

		public async Task UpdatePassword(AccountPasswordDTO accountPasswordDTO)
		{
			var account = await FindByID(accountPasswordDTO.AccountId);
			account.AccountPassword = PasswordHasher.Instance.Hash(accountPasswordDTO.AccountPassword);
			unitOfWork.GenericRepository.Update(account);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
