using BLL.DTOs;

namespace BLL.Services.Interfaces
{
	public interface INewsArticleService
	{
		Task Create(CreateNewsArticleDTO createNewsArticleDTO);
	}
}
