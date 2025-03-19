using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BLL.Services.Implements
{
	public class NewsArticleService(IUnitOfWork<NewsArticle> unitOfWork, IMapper mapper, ITagService tagService) : INewsArticleService
	{
		public async Task Create(CreateNewsArticleDTO createNewsArticleDTO)
		{
			var news = mapper.Map<NewsArticle>(createNewsArticleDTO);
			ICollection<Tag> tags = new List<Tag>();
			foreach (var item in createNewsArticleDTO.Tags)
			{
				tags.Add(await tagService.GetById(item));
			}
			news.Tags = tags;
			await unitOfWork.GenericRepository.Insert(news);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<NewsArticleDTO>> GetAll(string? search = null, int? categoryId = null)
		{
			Expression<Func<NewsArticle, bool>> predicate = x => (
				(string.IsNullOrWhiteSpace(search) || x.NewsTitle.ToLower() == search.ToLower() || x.Headline.ToLower() == search.ToLower()) &&
				(!categoryId.HasValue || x.CategoryId == categoryId)
			);
			string[] properties = new string[] { "Category", "CreatedBy", "Tags" };
			var newsArticles = await unitOfWork.GenericRepository.GetAll(predicate, null, properties);
			return mapper.Map<IEnumerable<NewsArticleDTO>>(newsArticles);
		}

		public async Task<NewsArticleDetailsDTO> GetDetails(int id)
		{
			string[] properties = new string[] { "Category", "CreatedBy", "UpdatedBy", "Tags" };
			var newsArticle = await FindById(id, properties);
			return mapper.Map<NewsArticleDetailsDTO>(newsArticle);
		}

		private async Task<NewsArticle?> FindById(int id, string[]? properties = null)
		{
			Expression<Func<NewsArticle, bool>> predicate = x => (
				x.NewsArticleId == id
			);
			return await unitOfWork.GenericRepository.Get(predicate, properties);
		}
	}
}
