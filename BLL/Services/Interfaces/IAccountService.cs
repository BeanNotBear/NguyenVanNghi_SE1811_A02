using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;
using System.Linq.Expressions;

namespace BLL.Services.Interfaces
{
	public interface IAccountService
	{
		Task<IEnumerable<SystemAccountDTO>> GetAll(string? search = null, AccountRole? role = null);
		Task Create(CreateAccountDTO createAccountDTO);
		Task Update(EditAccountDTO editAccountDTO);
		Task<SystemAccountDetailDTO> GetByID(int ID, string? properties = null);
	}
}
