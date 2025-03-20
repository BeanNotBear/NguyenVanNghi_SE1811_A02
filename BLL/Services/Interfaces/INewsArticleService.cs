using BLL.DTOs;
using Shared.Enums;

namespace BLL.Services.Interfaces
{
	public interface INewsArticleService
	{
		Task<IEnumerable<NewsArticleDTO>> GetAll(string? search = null, int? categoryId = null, NewsStatus? status = null);
		Task Create(CreateNewsArticleDTO createNewsArticleDTO);
		Task<NewsArticleDetailsDTO> GetDetails(int id);
		Task<EditNewsArticleDTO> GetById(int id);
		Task Update(EditNewsArticleDTO editNewsArticleDTO);
		Task Delete(int id);
	}
}
