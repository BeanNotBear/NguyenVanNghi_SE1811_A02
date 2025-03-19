using BLL.DTOs;

namespace BLL.Services.Interfaces
{
	public interface INewsArticleService
	{
		Task<IEnumerable<NewsArticleDTO>> GetAll(string? search = null, int? categoryId = null);
		Task Create(CreateNewsArticleDTO createNewsArticleDTO);
		Task<NewsArticleDetailsDTO> GetDetails(int id);
		Task<EditNewsArticleDTO> GetById(int id);
		Task Update(EditNewsArticleDTO editNewsArticleDTO);
	}
}
