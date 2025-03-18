using BLL.DTOs;
using Shared.Enums;
using System.Linq.Expressions;

namespace BLL.Services.Interfaces
{
	public interface IAccountService
	{
		Task<IEnumerable<SystemAccountDTO>> GetAll(string? search = null, AccountRole? role = null);
		Task Create(CreateAccountDTO createAccountDTO);
	}
}
