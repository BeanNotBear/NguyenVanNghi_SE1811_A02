using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services.Implements
{
	public class NewsArticleService(IUnitOfWork<NewsArticle> unitOfWork, IMapper mapper) : INewsArticleService
	{
		public async Task Create(CreateNewsArticleDTO createNewsArticleDTO)
		{
			await unitOfWork.GenericRepository.Insert(mapper.Map<NewsArticle>(createNewsArticleDTO));
			await unitOfWork.SaveChangesAsync();
		}
	}
}
