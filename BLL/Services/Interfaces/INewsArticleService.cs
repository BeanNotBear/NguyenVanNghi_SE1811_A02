using System;
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
		Task<IEnumerable<NewsArticleDTO>> GetList(string? search = null, int? categoryId = null, DateTime? startDate = null, DateTime? endDate = null, NewsStatus? status = null);
		Task<IEnumerable<NewsArticleDTO>> GetListByID(string? search = null, int? categoryId = null, NewsStatus? status = null, int? id = null);
	}
}
