using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;

namespace BLL.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryForSelectDTO>> GetAll();
        Task<IEnumerable<CategoryDTO>> GetAllCategory(string? search = null, CategoryStatus? status = null, string[]? properties = null);
    }
}
