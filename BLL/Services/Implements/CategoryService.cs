using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
	public class CategoryService(IUnitOfWork<Category> unitOfWork, IMapper mapper) : ICategoryService
	{
		public async Task<IEnumerable<CategoryForSelectDTO>> GetAll()
		{
			return mapper.Map<IEnumerable<CategoryForSelectDTO>>(await unitOfWork.GenericRepository.GetAll());
		}
	}
}
