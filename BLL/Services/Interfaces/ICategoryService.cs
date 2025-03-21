using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;

namespace BLL.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryForSelectDTO>> GetAll();
        Task<IEnumerable<CategoryDTO>> GetAllCategory(string? search = null, CategoryStatus? status = null, string[]? properties = null);
		Task Create(CategoryDTO createCategoryDTO);
		Task Update(CategoryDTO editCategoryDTO);
		Task<CategoryDTO> GetByID(int ID, string? properties = null);
		Task Delete(int id);
	}
}
