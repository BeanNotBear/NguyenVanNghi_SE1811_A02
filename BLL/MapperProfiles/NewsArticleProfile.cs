using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;

public class NewsArticleProfile : Profile
{
	public NewsArticleProfile()
	{
		CreateMap<CreateNewsArticleDTO, NewsArticle>()
			.ForMember(dest => dest.NewsStatus, opt => opt.MapFrom(src => (int)src.NewsStatus))
			.ForMember(dest => dest.Tags, opt => opt.Ignore());

		CreateMap<NewsArticle, NewsArticleDTO>()
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
			.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.AccountName));

		CreateMap<NewsArticle, NewsArticleDetailsDTO>()
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
			.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.AccountName))
			.ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.AccountName : ""))
			.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.TagName)));

		CreateMap<NewsArticle, EditNewsArticleDTO>()
			.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(x => x.TagId)));

		CreateMap<EditNewsArticleDTO, NewsArticle>()
			.ForMember(dest => dest.Tags, opt => opt.Ignore());
	}
}
