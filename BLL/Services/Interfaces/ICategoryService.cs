using BLL.DTOs;

namespace BLL.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryForSelectDTO>> GetAll();
	}
}
