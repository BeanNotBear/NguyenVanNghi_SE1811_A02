using System.Linq.Expressions;
using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Shared.Enums;

namespace BLL.Services.Implements
{
	public class CategoryService(IUnitOfWork<Category> unitOfWork, IMapper mapper) : ICategoryService
	{
		public async Task Create(CategoryDTO createCategoryDTO)
		{
			var category = mapper.Map<Category>(createCategoryDTO);
			await unitOfWork.GenericRepository.Insert(category);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var category = await FindByID(id);
			unitOfWork.GenericRepository.Delete(category);
			await unitOfWork.SaveChangesAsync();

		}

		public async Task<IEnumerable<CategoryForSelectDTO>> GetAll()
		{
			return mapper.Map<IEnumerable<CategoryForSelectDTO>>(await unitOfWork.GenericRepository.GetAll());
		}

		public async Task<IEnumerable<CategoryDTO>> GetAllCategory(string? search = null, CategoryStatus? status = null, string[]? properties = null)
		{
			Expression<Func<Category, bool>> predicate = x =>
			(
				(string.IsNullOrWhiteSpace(search) || x.CategoryName.ToLower().ToString().Contains(search.ToLower()))
				&& (!status.HasValue || (x.IsActive ? 1 : 0) == (int)(status.Value))
			);
			var categories = mapper.Map<IEnumerable<CategoryDTO>>(await unitOfWork.GenericRepository.GetAll(predicate, x => x.OrderBy(a => a.CategoryName), properties));

			return categories;
		}

		public async Task<CategoryDTO> GetByID(int ID, string? properties = null)
		{
			return mapper.Map<CategoryDTO>(await FindByID(ID, properties));
		}

		private async Task<Category?> FindByID(int ID, string? properties = null)
		{
			Expression<Func<Category, bool>> predicate = x =>
			(
				x.CategoryId == ID
			);
			string[]? joins = null;
			if (properties != null)
			{
				joins = properties.Split(',');
			}
			var category = await unitOfWork.GenericRepository.Get(predicate, joins);
			return category;
		}

		public async Task Update(CategoryDTO editCategoryDTO)
		{
			var category = await FindByID(editCategoryDTO.CategoryId);
			category.CategoryName = editCategoryDTO.CategoryName;
			category.CategoryDescription = editCategoryDTO.CategoryDescription;
			category.ParentCategoryId = editCategoryDTO.ParentCategoryId;
			category.IsActive = editCategoryDTO.CategoryStatus == CategoryStatus.Active;

			unitOfWork.GenericRepository.Update(category);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
