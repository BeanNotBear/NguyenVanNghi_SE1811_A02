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
        public async Task<IEnumerable<CategoryForSelectDTO>> GetAll()
        {
            return mapper.Map<IEnumerable<CategoryForSelectDTO>>(await unitOfWork.GenericRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory(string? search = null, CategoryStatus? status = null, string[]? properties = null)
        {
            Expression<Func<Category, bool>> predicate = x =>
            (
                (string.IsNullOrWhiteSpace(search) || x.CategoryName.ToLower().ToString().Contains(search.ToLower()))
                && (!status.HasValue || (x.IsActive ? 1 : 0)  == (int)(status.Value))
            );
			var categories = mapper.Map<IEnumerable<CategoryDTO>>(await unitOfWork.GenericRepository.GetAll(predicate, x => x.OrderBy(a => a.CategoryName), properties));

            return categories;
        }
    }
}
