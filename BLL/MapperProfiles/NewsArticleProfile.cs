using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using Shared.Enums;

namespace BLL.MapperProfiles
{
	public class NewsArticleProfile : Profile
	{
		public NewsArticleProfile()
		{
			CreateMap<CreateNewsArticleDTO, NewsArticle>().ForMember(dest => dest.NewsStatus, opt => opt.MapFrom(src => (int)(src.NewsStatus)));
		}
	}
}
